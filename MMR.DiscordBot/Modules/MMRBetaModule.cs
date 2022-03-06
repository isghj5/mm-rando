using Discord.Commands;
using MMR.DiscordBot.Services;
using MMR.DiscordBot.Attributes;

namespace MMR.DiscordBot.Modules
{
    [Group("mmrbeta")]
    [MMRReady(typeof(MMRBetaService))]
    public class MMRBetaModule : BaseMMRModule
    {
        public MMRBetaModule(MMRBetaService mmrService) : base(mmrService)
        {

        }
    }
}
