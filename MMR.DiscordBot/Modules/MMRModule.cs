using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MMR.DiscordBot.Data.Entities;
using MMR.DiscordBot.Data.Repositories;
using MMR.DiscordBot.Services;
using System.Collections.Generic;

namespace MMR.DiscordBot.Modules
{
    [Group("mmr")]
    public class MMRModule : BaseMMRModule
    {
        public MMRModule(MMRService mmrService) : base(mmrService)
        {

        }

        protected override void AddHelp(Dictionary<string, string> commands)
        {
            if (!Context.IsPrivate && Context.User is SocketGuildUser socketGuildUser)
            {
                if (socketGuildUser.GuildPermissions.Has(GuildPermission.Administrator))
                {
                    commands.Add("add-mod-role (<@role>)+", "Allow the tagged role(s) to use the setting commands.");
                    commands.Add("remove-mod-role (<@role>)+", "Disallow the tagged role(s) from using the setting commands.");
                }
            }
        }

        [Command("add-mod-role")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddModRole(IRole role)
        {
            var guildMod = await GuildModRepository.Single(gm => gm.RoleId == role.Id && gm.GuildId == Context.Guild.Id);
            if (guildMod != null)
            {
                await ReplyNoTagAsync("Role is already a mod role.");
                return;
            }
            await GuildModRepository.Save(new GuildModEntity
            {
                GuildId = Context.Guild.Id,
                RoleId = role.Id,
            });
            await ReplyNoTagAsync("Added");
        }

        [Command("remove-mod-role")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task RemoveModRole(IRole role)
        {
            var guildMod = await GuildModRepository.Single(gm => gm.RoleId == role.Id && gm.GuildId == Context.Guild.Id);
            if (guildMod == null)
            {
                await ReplyNoTagAsync("Role is already not a mod role.");
                return;
            }
            await GuildModRepository.DeleteById(guildMod.Id);
            await ReplyNoTagAsync("Removed");
        }

        [RequireOwner]
        [RequireContext(ContextType.Guild)]
        [Command("tournament")]
        public async Task ToggleTournament()
        {
            var tournamentChannel = await TournamentChannelRepository.Single(tc => tc.ChannelId == Context.Channel.Id);
            if (tournamentChannel == null)
            {
                await TournamentChannelRepository.Save(new TournamentChannelEntity
                {
                    ChannelId = Context.Channel.Id,
                });
                await ReplyNoTagAsync("Enabled tournament mode for this channel.");
            }
            else
            {
                await TournamentChannelRepository.DeleteById(tournamentChannel.Id);
                await ReplyNoTagAsync("Disabled tournament mode for this channel.");
            }
        }
    }
}
