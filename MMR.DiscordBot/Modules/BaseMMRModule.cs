using System;
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

namespace MMR.DiscordBot.Modules
{
    public class BaseMMRModule : ModuleBase<SocketCommandContext>
    {
        public UserSeedRepository UserSeedRepository { get; set; }
        public GuildModRepository GuildModRepository { get; set; }
        private readonly MMRService _mmrService;

        public BaseMMRModule(MMRService mmrService)
        {
            _mmrService = mmrService;
        }

        private readonly IReadOnlyCollection<ulong> _tournamentChannels = new List<ulong>
        {
            709731024375906316, // ZoeyZolotova - tournament-admin
            871199781454757969, // MMR - Season 2 Brackets - #bracket-seeds
        }.AsReadOnly();

        protected virtual void AddHelp(Dictionary<string, string> commands)
        {

        }

        [Command("help")]
        public async Task Help()
        {
            var commands = new Dictionary<string, string>()
            {
                {  "help", "See this help list." },
            };

            if (_tournamentChannels.Contains(Context.Channel.Id))
            {
                commands.Add("seed (<@user>){2,}", "Generate a seed. The patch and hashIcons will be sent in a direct message to the tagged users. The spoiler log will be sent to you.");
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
                    commands.Add("mystery <categoryName>", "Generate a seed using a random setting from the <categoryName> mystery category.");
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

            await ReplyAsync("List of commands: (all commands begin with \"!mmr\")\n" + string.Join('\n', commands.Select(kvp => $"`{kvp.Key}` - {kvp.Value}")));
        }

        private async Task TournamentSeed()
        {
            // Tournament seed
            var mentionedUsers = Context.Message.MentionedUsers.DistinctBy(u => u.Id);
            if (mentionedUsers.Any(u => u.Id == Context.User.Id))
            {
                await ReplyAsync("Cannot generate a seed for yourself.");
                return;
            }
            if (mentionedUsers.Count() < 2)
            {
                await ReplyAsync("Must mention at least two users.");
                return;
            }
            var tournamentSeedReply = await ReplyAsync("Generating seed...");
            new Thread(async () =>
            {
                try
                {
                    var (patchPath, hashIconPath, spoilerLogPath, _) = await _mmrService.GenerateSeed(DateTime.UtcNow, null);
                    if (File.Exists(patchPath) && File.Exists(hashIconPath) && File.Exists(spoilerLogPath))
                    {
                        foreach (var user in mentionedUsers)
                        {
                            await user.SendFileAsync(patchPath, "Here is your tournament match seed! Please be sure your Hash matches and let an organizer know if you have any issues before you begin.");
                            await user.SendFileAsync(hashIconPath);
                        }
                        await Context.User.SendFileAsync(spoilerLogPath);
                        await Context.User.SendFileAsync(hashIconPath);
                        File.Delete(spoilerLogPath);
                        File.Delete(patchPath);
                        File.Delete(hashIconPath);
                        await tournamentSeedReply.ModifyAsync(mp => mp.Content = "Success.");
                    }
                    else
                    {
                        throw new Exception("MMR.CLI succeeded, but output files not found.");
                    }
                }
                catch (Exception e)
                {
                    // TODO log exception.
                    await tournamentSeedReply.ModifyAsync(mp => mp.Content = "An error occured.");
                }
            }).Start();
        }

        private async Task VerifySeedFrequency()
        {
            var lastSeedRequest = (await UserSeedRepository.GetById(Context.User.Id))?.LastSeedRequest;
            if (lastSeedRequest.HasValue && (DateTime.UtcNow - lastSeedRequest.Value).TotalHours < 6)
            {
                await ReplyAsync("You may only request a seed once every 6 hours.");
                return;
            }
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
            var messageResult = await ReplyAsync("Generating seed...");
            new Thread(async () =>
            {
                try
                {
                    var (patchPath, hashIconPath, spoilerLogPath, version) = await _mmrService.GenerateSeed(now, settingPath);
                    await Context.Channel.SendFileAsync(patchPath);
                    await Context.Channel.SendFileAsync(hashIconPath);
                    File.Delete(patchPath);
                    File.Delete(hashIconPath);
                    await messageResult.DeleteAsync();
                    userSeedEntity.Version = version;
                    await UserSeedRepository.Save(userSeedEntity);
                }
                catch
                {
                    await UserSeedRepository.DeleteById(Context.User.Id);
                    await messageResult.ModifyAsync(mp => mp.Content = "An error occured.");
                }
            }).Start();
        }

        [Command("seed")]
        public async Task Seed([Remainder] string settingName = null)
        {
            if (_tournamentChannels.Contains(Context.Channel.Id))
            {
                await TournamentSeed();
                return;
            }
            await VerifySeedFrequency();
            string settingPath = null;
            if (!string.IsNullOrWhiteSpace(settingName))
            {
                settingPath = _mmrService.GetSettingsPath(Context.Guild.Id, settingName);
                if (!File.Exists(settingPath))
                {
                    await ReplyAsync("Setting not found.");
                    return;
                }
            }
            await GenerateSeed(settingPath);
        }

        [Command("spoiler")]
        public async Task Spoiler()
        {
            if (_tournamentChannels.Contains(Context.Channel.Id))
            {
                // Silently ignore.
                return;
            }

            var userSeedEntity = await UserSeedRepository.GetById(Context.User.Id);
            if (userSeedEntity == null)
            {
                await ReplyAsync("You haven't generated any seeds recently.");
                return;
            }
            var (_, _, _, spoilerLogPath, _) = _mmrService.GetSeedPaths(userSeedEntity.LastSeedRequest, userSeedEntity.Version ?? "1.13.0.13");
            if (File.Exists(spoilerLogPath))
            {
                var result = await Context.Channel.SendFileAsync(spoilerLogPath);
                File.Delete(spoilerLogPath);
            }
            else
            {
                await ReplyAsync("Spoiler log not found.");
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
                await ReplyAsync("You don't have permission.");
                return;
            }
            //await ReplyAsync("Allowed");

            if (Context.Message.Attachments.Count != 1)
            {
                await ReplyAsync("Must attach one settings json file.");
                return;
            }

            var settingsFile = Context.Message.Attachments.Single();
            if (settingsFile.Size > 10000) // kinda arbitrary
            {
                await ReplyAsync("File is too large.");
                return;
            }

            if (Path.GetExtension(settingsFile.Filename) != ".json")
            {
                await ReplyAsync("File must be a json file.");
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

            await ReplyAsync($"{(replacing ? "Replaced" : "Added")} settings.");
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
                await ReplyAsync("You don't have permission.");
                return;
            }
            //await ReplyAsync("Allowed");

            var settingsPath = _mmrService.GetSettingsPath(Context.Guild.Id, settingName);
            if (!File.Exists(settingsPath))
            {
                await ReplyAsync("Setting does not exist.");
                return;
            }

            File.Delete(settingsPath);

            await ReplyAsync("Deleted settings.");
        }

        [Command("list-settings")]
        [RequireContext(ContextType.Guild)]
        public async Task ListSettings()
        {
            var settingsPaths = _mmrService.GetSettingsPaths(Context.Guild.Id);

            if (!settingsPaths.Any())
            {
                await ReplyAsync("No settings found.");
                return;
            }

            await ReplyAsync("List of settings:\n" + string.Join('\n', settingsPaths.Select(p => Path.GetFileNameWithoutExtension(p))));
        }

        [Command("get-settings")]
        [RequireContext(ContextType.Guild)]
        public async Task GetSettings([Remainder] string settingName)
        {
            var settingsPath = _mmrService.GetSettingsPath(Context.Guild.Id, settingName);
            if (!File.Exists(settingsPath))
            {
                await ReplyAsync("Setting does not exist.");
                return;
            }

            await Context.Channel.SendFileAsync(settingsPath);
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
                await ReplyAsync("You don't have permission.");
                return;
            }
            //await ReplyAsync("Allowed");

            if (Context.Message.Attachments.Count != 1)
            {
                await ReplyAsync("Must attach one settings json file.");
                return;
            }

            var settingsFile = Context.Message.Attachments.Single();
            if (settingsFile.Size > 10000) // kinda arbitrary
            {
                await ReplyAsync("File is too large.");
                return;
            }

            if (Path.GetExtension(settingsFile.Filename) != ".json")
            {
                await ReplyAsync("File must be a json file.");
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

            await ReplyAsync($"{(replacing ? "Replaced" : "Added")} mystery setting.");
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
                await ReplyAsync("You don't have permission.");
                return;
            }
            //await ReplyAsync("Allowed");

            if (argument.Length != 2)
            {
                await ReplyAsync("Invalid parameter count.");
                return;
            }

            var categoryName = argument[0];
            var settingName = argument[1];

            var mysteryPath = _mmrService.GetMysteryPath(Context.Guild.Id, categoryName, false);
            var settingPath = Path.Combine(mysteryPath, settingName);
            if (!File.Exists(settingPath))
            {
                await ReplyAsync("Setting does not exist.");
                return;
            }

            File.Delete(settingPath);

            await ReplyAsync("Deleted mystery setting.");
        }

        [Command("list-mystery")]
        [RequireContext(ContextType.Guild)]
        public async Task ListMystery(string categoryName = null)
        {
            var mysteryRoot = categoryName == null ? _mmrService.GetMysteryRoot(Context.Guild.Id) : _mmrService.GetMysteryPath(Context.Guild.Id, categoryName, false);
            if (!Directory.Exists(mysteryRoot))
            {
                await ReplyAsync("No settings found.");
                return;
            }

            var settingsPaths = categoryName == null ? Directory.EnumerateDirectories(mysteryRoot) : Directory.EnumerateFiles(mysteryRoot);

            if (!settingsPaths.Any())
            {
                await ReplyAsync("No settings found.");
                return;
            }

            await ReplyAsync("List of settings:\n" + string.Join('\n', settingsPaths.Select(p => Path.GetFileNameWithoutExtension(p))));
        }

        [Command("get-mystery")]
        [RequireContext(ContextType.Guild)]
        public async Task GetMystery(params string[] argument)
        {
            if (argument.Length != 2)
            {
                await ReplyAsync("Invalid parameter count.");
                return;
            }

            var categoryName = argument[0];
            var settingName = argument[1];

            var mysteryPath = _mmrService.GetMysteryPath(Context.Guild.Id, categoryName, false);
            var settingPath = Path.Combine(mysteryPath, settingName);

            if (!File.Exists(settingPath))
            {
                await ReplyAsync("Setting does not exist.");
                return;
            }

            await Context.Channel.SendFileAsync(settingPath);
        }

        [Command("mystery")]
        public async Task MysterySeed([Remainder] string categoryName)
        {
            await VerifySeedFrequency();
            var mysteryPath = _mmrService.GetMysteryPath(Context.Guild.Id, categoryName, false);
            if (!Directory.Exists(mysteryPath))
            {
                await ReplyAsync("Mystery category not found.");
                return;
            }

            var settingFiles = Directory.EnumerateFiles(mysteryPath).ToList();
            var settingPath = settingFiles.RandomOrDefault(new Random());
            if (settingPath == default)
            {
                await ReplyAsync("Mystery category not found.");
                return;
            }
            await GenerateSeed(settingPath);
        }
    }
}
