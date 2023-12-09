﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MMR.DiscordBot.Data.Entities;
using MMR.DiscordBot.Data.Repositories;
using MMR.DiscordBot.Services;
using MMR.Common.Extensions;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using MMR.Common.Utils;
using System.Diagnostics;

namespace MMR.DiscordBot.Modules
{
    public abstract class BaseMMRModule : ModuleBase<SocketCommandContext>
    {
        public UserSeedRepository UserSeedRepository { get; set; }
        public GuildModRepository GuildModRepository { get; set; }
        public TournamentChannelRepository TournamentChannelRepository { get; set; }
        public TournamentSeedRepository TournamentSeedRepository { get; set; }
        public LogChannelRepository LogChannelRepository { get; set; }

        private readonly MMRBaseService _mmrService;

        public BaseMMRModule(MMRBaseService mmrService)
        {
            _mmrService = mmrService;
        }

        protected virtual void AddHelp(Dictionary<string, string> commands)
        {

        }

        protected async Task<IUserMessage> ReplyNoTagAsync(string message)
        {
            var messageReference = new MessageReference(Context.Message.Id, Context.Channel.Id);
            return await ReplyAsync(message, allowedMentions: AllowedMentions.None, messageReference: messageReference);
        }

        protected async Task ModifyNoTagAsync(IUserMessage message, Action<MessageProperties> func)
        {
            await message.ModifyAsync(mp =>
            {
                func(mp);
                mp.AllowedMentions = AllowedMentions.None;
            });
        }

        protected async Task<Discord.Rest.RestUserMessage> ReplySendFileAsync(string filepath)
        {
            var messageReference = new MessageReference(Context.Message.Id, Context.Channel.Id, Context.Guild?.Id);
            return await Context.Channel.SendFileAsync(filepath, allowedMentions: AllowedMentions.None, messageReference: messageReference);
        }

        protected async Task LogToDiscord(string message)
        {
            var logChannel = await LogChannelRepository.Single(_ => true);
            if (logChannel == null)
            {
                return;
            }
            var channel = Context.Client.GetChannel(logChannel.ChannelId) as IMessageChannel;
            await channel.SendMessageAsync($"<t:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}:f> [{_mmrService.GetType().Name}] - {message}");
        }

        [Command("help")]
        public async Task Help()
        {
            var commands = new Dictionary<string, string>()
            {
                {  "help", "See this help list." },
                {  "version", "See the MMR version for this command module." },
            };

            var logChannel = await LogChannelRepository.Single(_ => true);
            if (logChannel != null && Context.Channel.Id == logChannel.ChannelId)
            {
                commands.Add("kill", "Kill the current seed generation for this module.");
            }

            if (await TournamentChannelRepository.ExistsByChannelId(Context.Channel.Id))
            {
                commands.Add("seed (<settingName>)? (<@user>){2,}", "Generate a seed. Optionally provide a setting name. The patch and hashIcons will be sent in a direct message to the tagged users. The spoiler log will be sent to you.");
                commands.Add("prepare (<settingName>)?", "Generate a seed. Optionally provide a setting name. The patch, hashIcons and spoiler log will be stored until send by the `send` command.");
                commands.Add("send (<@user>){2,}", "Send a prepared seed. The patch and hashIcons will be sent in a direct message to the tagged users. The spoiler log will be sent to you.");
            }
            else
            {
                if (Context.IsPrivate)
                {
                    commands.Add("seed", "Generate a seed.");
                }
                else
                {
                    commands.Add("seed (<settingName>)?", "Generate a seed. Optionally provide a setting name.");
                }
                commands.Add("spoiler", "Retrieve the spoiler log for your last generated seed.");
            }

            if (!Context.IsPrivate && Context.User is SocketGuildUser socketGuildUser)
            {
                var allowedRoles = await GuildModRepository.ListByGuildId(Context.Guild.Id);
                var allowedRoleIds = allowedRoles.Select(r => r.RoleId);
                if (socketGuildUser.Roles.Any(sr => allowedRoleIds.Contains(sr.Id)))
                {
                    commands.Add("add-settings <settingName>", "Upload a settings file and name it.");
                    commands.Add("remove-settings <settingName>", "Remove a named setting.");
                }
                commands.Add("list-settings", "List the names of available settings.");
                commands.Add("get-settings <settingName>", "Get a setting file.");

                if (socketGuildUser.Roles.Any(sr => allowedRoleIds.Contains(sr.Id)))
                {
                    commands.Add("add-mystery <categoryName>", "Upload a settings file and add it to the <categoryName> category.");
                    commands.Add("remove-mystery <categoryName> <settingName>", "Remove <settingName> from the <categoryName> mystery category.");
                }
                commands.Add("list-mystery (<categoryName>)?", "List the names of available mystery categories, or list the settings within a mystery category.");
                commands.Add("get-mystery <categoryName> <settingName>", "Get the <settingName> setting from the <categoryName> mystery category.");
            }

            AddHelp(commands);

            await ReplyNoTagAsync("List of commands: (all commands begin with \"!mmr\")\n" + string.Join('\n', commands.Select(kvp => $"`{kvp.Key}` - {kvp.Value}")));
        }

        private async Task PrepareTournamentSeed(string settingPath)
        {
            var now = DateTime.UtcNow;
            var tournamentSeedEntity = new TournamentSeedEntity
            {
                UserId = Context.User.Id,
                DateTime = now
            };
            await TournamentSeedRepository.Save(tournamentSeedEntity);
            var tournamentSeedReply = await ReplyNoTagAsync("Generating seed...");
            await LogToDiscord($"User {Context.User.Username} requested to prepare a tournament seed.");
            new Thread(async () =>
            {
                try
                {
                    var (patchPath, hashIconPath, spoilerLogPath, version) = await _mmrService.GenerateSeed(now, settingPath, async (i) =>
                    {
                        if (i < 0)
                        {
                            await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = "Generating seed...");
                        }
                        else
                        {
                            await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = $"You are number {i + 1} in the queue.");
                        }
                    });
                    if (File.Exists(patchPath) && File.Exists(hashIconPath) && File.Exists(spoilerLogPath))
                    {
                        tournamentSeedEntity.Version = version;
                        await TournamentSeedRepository.Save(tournamentSeedEntity);
                        await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = "Success.");
                        await LogToDiscord($"User {Context.User.Username} tournament seed successfully prepared.");
                    }
                    else
                    {
                        throw new Exception("MMR.CLI succeeded, but output files not found.");
                    }
                }
                catch (Exception ex)
                {
                    await TournamentSeedRepository.DeleteById(Context.User.Id);
                    await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = "An error occurred.");
                    await LogToDiscord($"User {Context.User.Username} tournament seed failed to generate. Error: {ex.Message}");
                }
            }).Start();
        }

        private async Task SendPreparedTournamentSeed(DateTime dateTime, string version, IUser[] users)
        {
            if (users.Any(u => u.Id == Context.User.Id))
            {
                await ReplyNoTagAsync("Cannot send a seed to yourself.");
                return;
            }
            if (users.Count() < 2)
            {
                await ReplyNoTagAsync("Must mention at least two users.");
                return;
            }

            var tournamentSeedReply = await ReplyNoTagAsync("Sending seed...");
            await LogToDiscord($"User {Context.User.Username} requested to send a prepared tournament seed.");
            var (_, patchPath, hashIconPath, spoilerLogPath, _) = _mmrService.GetSeedPaths(dateTime, version);
            try
            {
                if (File.Exists(patchPath) && File.Exists(hashIconPath) && File.Exists(spoilerLogPath))
                {
                    foreach (var user in users)
                    {
                        await user.SendFileAsync(patchPath, "Here is your tournament match seed! Please be sure your Hash matches and let an organizer know if you have any issues before you begin.");
                        await user.SendFileAsync(hashIconPath);
                    }
                    await Context.User.SendFileAsync(spoilerLogPath);
                    await Context.User.SendFileAsync(hashIconPath);
                    await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = "Success.");
                    await LogToDiscord($"User {Context.User.Username} successfully sent a prepared tournament seed.");
                }
                else
                {
                    throw new Exception("Files not found.");
                }
            }
            catch (Exception ex)
            {
                await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = "An error occurred.");
                await LogToDiscord($"User {Context.User.Username} failed to send a prepared tournament seed. Error: {ex.Message}");
            }
            finally
            {
                File.Delete(spoilerLogPath);
                File.Delete(patchPath);
                File.Delete(hashIconPath);
                await TournamentSeedRepository.DeleteById(Context.User.Id);
            }
        }

        private async Task TournamentSeed(string settingPath, IUser[] users)
        {
            if (users.Any(u => u.Id == Context.User.Id))
            {
                await ReplyNoTagAsync("Cannot generate a seed for yourself.");
                return;
            }
            if (users.Count() < 2)
            {
                await ReplyNoTagAsync("Must mention at least two users.");
                return;
            }
            var tournamentSeedReply = await ReplyNoTagAsync("Generating seed...");
            await LogToDiscord($"User {Context.User.Username} requested a tournament seed.");
            new Thread(async () =>
            {
                try
                {
                    var (patchPath, hashIconPath, spoilerLogPath, _) = await _mmrService.GenerateSeed(DateTime.UtcNow, settingPath, async (i) =>
                    {
                        if (i < 0)
                        {
                            await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = "Generating seed...");
                        }
                        else
                        {
                            await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = $"You are number {i + 1} in the queue.");
                        }
                    });
                    if (File.Exists(patchPath) && File.Exists(hashIconPath) && File.Exists(spoilerLogPath))
                    {
                        foreach (var user in users)
                        {
                            await user.SendFileAsync(patchPath, "Here is your tournament match seed! Please be sure your Hash matches and let an organizer know if you have any issues before you begin.");
                            await user.SendFileAsync(hashIconPath);
                        }
                        await Context.User.SendFileAsync(spoilerLogPath);
                        await Context.User.SendFileAsync(hashIconPath);
                        File.Delete(spoilerLogPath);
                        File.Delete(patchPath);
                        File.Delete(hashIconPath);
                        await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = "Success.");
                        await LogToDiscord($"User {Context.User.Username} tournament seed successfully generated.");
                    }
                    else
                    {
                        throw new Exception("MMR.CLI succeeded, but output files not found.");
                    }
                }
                catch (Exception ex)
                {
                    await ModifyNoTagAsync(tournamentSeedReply, mp => mp.Content = "An error occurred.");
                    await LogToDiscord($"User {Context.User.Username} tournament seed failed to generate. Error: {ex.Message}");
                }
            }).Start();
        }

        private async Task<bool> VerifySeedFrequency()
        {
            var lastSeedRequest = (await UserSeedRepository.GetById(Context.User.Id))?.LastSeedRequest;
            if (lastSeedRequest.HasValue && (DateTime.UtcNow - lastSeedRequest.Value).TotalHours < 6)
            {
                await ReplyNoTagAsync("You may only request a seed once every 6 hours.");
                return false;
            }

            return true;
        }

        private async Task GenerateSeed(string settingPath)
        {
            var now = DateTime.UtcNow;
            var userSeedEntity = new UserSeedEntity
            {
                UserId = Context.User.Id,
                LastSeedRequest = now
            };
            await UserSeedRepository.Save(userSeedEntity);
            var messageResult = await ReplyNoTagAsync("Generating seed...");
            await LogToDiscord($"User {Context.User.Username} requested a seed.");
            new Thread(async () =>
            {
                try
                {
                    var stopwatch = new Stopwatch();
                    var (patchPath, hashIconPath, spoilerLogPath, version) = await _mmrService.GenerateSeed(now, settingPath, async (i) =>
                    {
                        if (i < 0)
                        {
                            stopwatch.Start();
                            await ModifyNoTagAsync(messageResult, mp => mp.Content = "Generating seed...");
                        }
                        else
                        {
                            await ModifyNoTagAsync(messageResult, mp => mp.Content = $"You are number {i + 1} in the queue.");
                        }
                    });
                    stopwatch.Stop();
                    var filesToSend = new List<FileAttachment>
                    {
                        new FileAttachment(patchPath),
                        new FileAttachment(hashIconPath)
                    };
                    await ModifyNoTagAsync(messageResult, mp =>
                    {
                        mp.Content = $"Completed in {stopwatch.Elapsed}";
                        mp.Attachments = filesToSend;
                    });
                    File.Delete(patchPath);
                    File.Delete(hashIconPath);
                    userSeedEntity.Version = version;
                    await UserSeedRepository.Save(userSeedEntity);
                    await LogToDiscord($"User {Context.User.Username} seed successfully generated.");
                }
                catch (Exception ex)
                {
                    await UserSeedRepository.DeleteById(Context.User.Id);
                    await ModifyNoTagAsync(messageResult, mp => mp.Content = "An error occured.");
                    await LogToDiscord($"User {Context.User.Username} seed failed to generate. Error: {ex.Message}");
                }
            }).Start();
        }

        private async Task<(string settingPath, bool success)> GetSettingPath(string settingName)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                return (null, true);
            }

            if (Context.Guild == null)
            {
                await ReplyNoTagAsync("Settings are unavailable in direct messages.");
                return (null, false);
            }

            var settingPath = _mmrService.GetSettingsPath(Context.Guild.Id, settingName);
            if (File.Exists(settingPath))
            {
                return (settingPath, true);
            }

            var mysteryPath = _mmrService.GetMysteryPath(Context.Guild.Id, settingName, false);
            if (Directory.Exists(mysteryPath))
            {
                var settingFiles = Directory.EnumerateFiles(mysteryPath).ToList();
                settingPath = settingFiles.RandomOrDefault(new Random());
                if (settingPath != default)
                {
                    return (settingPath, true);
                }
            }

            await ReplyNoTagAsync("Setting not found.");
            return (null, false);
        }

        [Command("prepare")]
        public async Task PrepareSeed(string settingName = null)
        {
            if (!await TournamentChannelRepository.ExistsByChannelId(Context.Channel.Id))
            {
                return;
            }

            var (settingPath, success) = await GetSettingPath(settingName);
            if (!success)
            {
                return;
            }

            var tournamentSeed = await TournamentSeedRepository.GetById(Context.User.Id);
            if (tournamentSeed != null)
            {
                if (string.IsNullOrWhiteSpace(tournamentSeed.Version))
                {
                    await ReplyNoTagAsync("Seed is still generating.");
                }
                else
                {
                    await ReplyNoTagAsync("You already have a seed prepared.");
                }
            }
            else
            {
                await PrepareTournamentSeed(settingPath);
            }
        }

        [Command("send")]
        public async Task SendSeed(params IUser[] users)
        {
            if (!await TournamentChannelRepository.ExistsByChannelId(Context.Channel.Id))
            {
                return;
            }

            var tournamentSeed = await TournamentSeedRepository.GetById(Context.User.Id);
            if (tournamentSeed != null)
            {
                if (string.IsNullOrWhiteSpace(tournamentSeed.Version))
                {
                    await ReplyNoTagAsync("Seed is still generating.");
                }
                else
                {
                    await SendPreparedTournamentSeed(tournamentSeed.DateTime, tournamentSeed.Version, users);
                }
            }
            else
            {
                await ReplyNoTagAsync("You do not have a seed prepared.");
            }
        }

        [Command("seed")]
        public async Task Seed(params IUser[] users)
        {
            await Seed(null, users);
        }

        [Command("seed")]
        public async Task Seed(string settingName = null, params IUser[] users)
        {
            var (settingPath, success) = await GetSettingPath(settingName);
            if (!success)
            {
                return;
            }

            if (await TournamentChannelRepository.ExistsByChannelId(Context.Channel.Id))
            {
                await TournamentSeed(settingPath, users);
                return;
            }
            if (!await VerifySeedFrequency())
            {
                return;
            }

            await GenerateSeed(settingPath);
        }

        [Command("spoiler")]
        public async Task Spoiler()
        {
            if (await TournamentChannelRepository.ExistsByChannelId(Context.Channel.Id))
            {
                // Silently ignore.
                return;
            }

            var userSeedEntity = await UserSeedRepository.GetById(Context.User.Id);
            if (userSeedEntity == null)
            {
                await ReplyNoTagAsync("You haven't generated any seeds recently.");
                return;
            }
            var (_, _, _, spoilerLogPath, _) = _mmrService.GetSeedPaths(userSeedEntity.LastSeedRequest, userSeedEntity.Version ?? "1.13.0.13");
            if (File.Exists(spoilerLogPath))
            {
                await ReplySendFileAsync(spoilerLogPath);
                File.Delete(spoilerLogPath);
            }
            else
            {
                await ReplyNoTagAsync("Spoiler log not found.");
            }
        }

        [Command("add-settings")]
        [RequireContext(ContextType.Guild)]
        public async Task AddSettings([Remainder] string settingName)
        {
            var allowedRoles = await GuildModRepository.ListByGuildId(Context.Guild.Id);
            var allowedRoleIds = allowedRoles.Select(r => r.RoleId);
            var socketGuildUser = Context.User as SocketGuildUser;
            if (!socketGuildUser.Roles.Any(sr => allowedRoleIds.Contains(sr.Id)))
            {
                await ReplyNoTagAsync("You don't have permission.");
                return;
            }
            //await ReplyAsync("Allowed");

            if (Context.Message.Attachments.Count != 1)
            {
                await ReplyNoTagAsync("Must attach one settings json file.");
                return;
            }

            var settingsFile = Context.Message.Attachments.Single();
            if (settingsFile.Size > 25 * 1024) // kinda arbitrary
            {
                await ReplyNoTagAsync("File is too large.");
                return;
            }

            if (Path.GetExtension(settingsFile.Filename) != ".json")
            {
                await ReplyNoTagAsync("File must be a json file.");
                return;
            }

            var mysteryPath = _mmrService.GetMysteryPath(Context.Guild.Id, settingName, false);
            if (Directory.Exists(mysteryPath))
            {
                await ReplyNoTagAsync("A mystery category with that name already exists.");
                return;
            }

            var replacing = false;
            var settingsPath = _mmrService.GetSettingsPath(Context.Guild.Id, settingName);
            if (File.Exists(settingsPath))
            {
                replacing = true;
            }

            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(settingsFile.Url), settingsPath);
            }

            await ReplyNoTagAsync($"{(replacing ? "Replaced" : "Added")} settings.");
        }

        [Command("remove-settings")]
        [RequireContext(ContextType.Guild)]
        public async Task RemoveSettings([Remainder] string settingName)
        {
            var allowedRoles = await GuildModRepository.ListByGuildId(Context.Guild.Id);
            var allowedRoleIds = allowedRoles.Select(r => r.RoleId);
            var socketGuildUser = Context.User as SocketGuildUser;
            if (!socketGuildUser.Roles.Any(sr => allowedRoleIds.Contains(sr.Id)))
            {
                await ReplyNoTagAsync("You don't have permission.");
                return;
            }
            //await ReplyAsync("Allowed");

            var settingsPath = _mmrService.GetSettingsPath(Context.Guild.Id, settingName);
            if (!File.Exists(settingsPath))
            {
                await ReplyNoTagAsync("Setting does not exist.");
                return;
            }

            File.Delete(settingsPath);

            await ReplyNoTagAsync("Deleted settings.");
        }

        [Command("list-settings")]
        [RequireContext(ContextType.Guild)]
        public async Task ListSettings()
        {
            var settings = _mmrService.GetSettingsPaths(Context.Guild.Id)
                .Select(p => Path.GetFileNameWithoutExtension(p))
                .ToList();

            var mysteryRoot = _mmrService.GetMysteryRoot(Context.Guild.Id);
            if (Directory.Exists(mysteryRoot))
            {
                var mysteryCategories = Directory.EnumerateDirectories(mysteryRoot);

                settings.AddRange(mysteryCategories.Select(categoryFolder =>
                {
                    var fileCount = Directory.EnumerateFiles(categoryFolder).Count();
                    return $"{Path.GetFileNameWithoutExtension(categoryFolder)} ({fileCount} file{(fileCount != 1 ? "s" : "")})";
                }));
            }

            if (!settings.Any())
            {
                await ReplyNoTagAsync("No settings found.");
                return;
            }

            await ReplyNoTagAsync("List of settings:\n" + string.Join('\n', settings));
        }

        [Command("get-settings")]
        [RequireContext(ContextType.Guild)]
        public async Task GetSettings([Remainder] string settingName)
        {
            var settingsPath = _mmrService.GetSettingsPath(Context.Guild.Id, settingName);
            if (!File.Exists(settingsPath))
            {
                await ReplyNoTagAsync("Setting does not exist.");
                return;
            }

            await ReplySendFileAsync(settingsPath);
        }

        [Command("add-mystery")]
        [RequireContext(ContextType.Guild)]
        public async Task AddMystery([Remainder] string categoryName)
        {
            var allowedRoles = await GuildModRepository.ListByGuildId(Context.Guild.Id);
            var allowedRoleIds = allowedRoles.Select(r => r.RoleId);
            var socketGuildUser = Context.User as SocketGuildUser;
            if (!socketGuildUser.Roles.Any(sr => allowedRoleIds.Contains(sr.Id)))
            {
                await ReplyNoTagAsync("You don't have permission.");
                return;
            }
            //await ReplyAsync("Allowed");

            if (Context.Message.Attachments.Count != 1)
            {
                await ReplyNoTagAsync("Must attach one settings json file.");
                return;
            }

            var settingsFile = Context.Message.Attachments.Single();
            if (settingsFile.Size > 10000) // kinda arbitrary
            {
                await ReplyNoTagAsync("File is too large.");
                return;
            }

            if (Path.GetExtension(settingsFile.Filename) != ".json")
            {
                await ReplyNoTagAsync("File must be a json file.");
                return;
            }

            var settingsPath = _mmrService.GetSettingsPath(Context.Guild.Id, categoryName);
            if (File.Exists(settingsPath))
            {
                await ReplyNoTagAsync("A non-mystery setting with that name already exists.");
                return;
            }

            var replacing = false;
            var mysteryPath = _mmrService.GetMysteryPath(Context.Guild.Id, categoryName, true);
            var settingPath = Path.Combine(mysteryPath, settingsFile.Filename);
            if (File.Exists(settingPath))
            {
                replacing = true;
            }

            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(settingsFile.Url), settingPath);
            }

            await ReplyNoTagAsync($"{(replacing ? "Replaced" : "Added")} mystery setting.");
        }

        [Command("remove-mystery")]
        [RequireContext(ContextType.Guild)]
        public async Task RemoveMystery(params string[] argument)
        {
            var allowedRoles = await GuildModRepository.ListByGuildId(Context.Guild.Id);
            var allowedRoleIds = allowedRoles.Select(r => r.RoleId);
            var socketGuildUser = Context.User as SocketGuildUser;
            if (!socketGuildUser.Roles.Any(sr => allowedRoleIds.Contains(sr.Id)))
            {
                await ReplyNoTagAsync("You don't have permission.");
                return;
            }
            //await ReplyAsync("Allowed");

            if (argument.Length != 2)
            {
                await ReplyNoTagAsync("Invalid parameter count.");
                return;
            }

            var categoryName = argument[0];
            var settingName = argument[1];

            var mysteryPath = _mmrService.GetMysteryPath(Context.Guild.Id, categoryName, false);
            var settingPath = Path.Combine(mysteryPath, $"{FileUtils.MakeFilenameValid(settingName)}.json");
            if (!File.Exists(settingPath))
            {
                await ReplyNoTagAsync("Setting does not exist.");
                return;
            }

            File.Delete(settingPath);

            if (Directory.EnumerateFiles(mysteryPath).Count() == 0)
            {
                Directory.Delete(mysteryPath);
            }

            await ReplyNoTagAsync("Deleted mystery setting.");
        }

        [Command("list-mystery")]
        [RequireContext(ContextType.Guild)]
        public async Task ListMystery(string categoryName = null)
        {
            var mysteryRoot = categoryName == null ? _mmrService.GetMysteryRoot(Context.Guild.Id) : _mmrService.GetMysteryPath(Context.Guild.Id, categoryName, false);
            if (!Directory.Exists(mysteryRoot))
            {
                await ReplyNoTagAsync("No settings found.");
                return;
            }

            var settingsPaths = categoryName == null ? Directory.EnumerateDirectories(mysteryRoot) : Directory.EnumerateFiles(mysteryRoot);

            if (!settingsPaths.Any())
            {
                await ReplyNoTagAsync("No settings found.");
                return;
            }

            await ReplyNoTagAsync("List of settings:\n" + string.Join('\n', settingsPaths.Select(p => Path.GetFileNameWithoutExtension(p))));
        }

        [Command("get-mystery")]
        [RequireContext(ContextType.Guild)]
        public async Task GetMystery(params string[] argument)
        {
            if (argument.Length != 2)
            {
                await ReplyNoTagAsync("Invalid parameter count.");
                return;
            }

            var categoryName = argument[0];
            var settingName = argument[1];

            var mysteryPath = _mmrService.GetMysteryPath(Context.Guild.Id, categoryName, false);
            var settingPath = Path.Combine(mysteryPath, $"{FileUtils.MakeFilenameValid(settingName)}.json");

            if (!File.Exists(settingPath))
            {
                await ReplyNoTagAsync("Setting does not exist.");
                return;
            }

            await ReplySendFileAsync(settingPath);
        }

        [Command("mystery")]
        [RequireContext(ContextType.Guild)]
        public async Task MysterySeed([Remainder] string categoryName)
        {
            await ReplyNoTagAsync("Warning - this command is deprecated and will be removed in the future. Use the `seed` command instead.");
            if (!await VerifySeedFrequency())
            {
                return;
            }
            var mysteryPath = _mmrService.GetMysteryPath(Context.Guild.Id, categoryName, false);
            if (!Directory.Exists(mysteryPath))
            {
                await ReplyNoTagAsync("Mystery category not found.");
                return;
            }

            var settingFiles = Directory.EnumerateFiles(mysteryPath).ToList();
            var settingPath = settingFiles.RandomOrDefault(new Random());
            if (settingPath == default)
            {
                await ReplyNoTagAsync("Mystery category not found.");
                return;
            }
            await GenerateSeed(settingPath);
        }

        [Command("set-default-settings")]
        [RequireOwner]
        public async Task SetDefaultSettings()
        {
            if (Context.Message.Attachments.Count != 1)
            {
                await ReplyNoTagAsync("Must attach one settings json file.");
                return;
            }

            var settingsFile = Context.Message.Attachments.Single();

            if (Path.GetExtension(settingsFile.Filename) != ".json")
            {
                await ReplyNoTagAsync("File must be a json file.");
                return;
            }

            var replacing = false;
            var settingsPath = _mmrService.GetDefaultSettingsPath();
            if (File.Exists(settingsPath))
            {
                replacing = true;
            }

            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(settingsFile.Url), settingsPath);
            }

            await ReplyNoTagAsync($"{(replacing ? "Replaced" : "Added")} default settings.");
        }

        [Command("delete-default-settings")]
        [RequireOwner]
        public async Task DeleteDefaultSettings()
        {
            var settingsPath = _mmrService.GetDefaultSettingsPath();
            if (!File.Exists(settingsPath))
            {
                await ReplyNoTagAsync("No default setting file is set.");
                return;
            }

            File.Delete(settingsPath);

            await ReplyNoTagAsync("Deleted default settings.");
        }

        [Command("kill")]
        public async Task Kill()
        {
            var logChannel = await LogChannelRepository.Single(_ => true);
            if (logChannel != null && Context.Channel.Id == logChannel.ChannelId)
            {
                _mmrService.Kill();
            }
        }

        [Command("version")]
        public async Task Version()
        {
            var version = _mmrService.GetVersion();
            await ReplyNoTagAsync(version);
        }
    }
}
