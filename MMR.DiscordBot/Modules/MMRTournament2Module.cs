using Discord.Commands;
using MMR.DiscordBot.Services;
using MMR.DiscordBot.Attributes;

namespace MMR.DiscordBot.Modules
{
    [Group("mmrt2")]
    [MMRReady(typeof(MMRTournament2Service))]
    public class MMRTournament2Module : BaseMMRModule
    {
        public MMRTournament2Module(MMRTournament2Service mmrService) : base(mmrService)
        {

        }
    }
}
