using Discord.Commands;
using MMR.DiscordBot.Services;
using MMR.DiscordBot.Attributes;

namespace MMR.DiscordBot.Modules
{
    [Group("mmrt1")]
    [MMRReady(typeof(MMRTournament1Service))]
    public class MMRTournament1Module : BaseMMRModule
    {
        public MMRTournament1Module(MMRTournament1Service mmrService) : base(mmrService)
        {

        }
    }
}
