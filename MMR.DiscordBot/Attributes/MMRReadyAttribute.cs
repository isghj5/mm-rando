using Discord.Commands;
using MMR.DiscordBot.Services;
using System;
using System.Threading.Tasks;

namespace MMR.DiscordBot.Attributes
{
    public class MMRReadyAttribute : PreconditionAttribute
    {
        private readonly Type _mmrServiceType;

        public MMRReadyAttribute(Type mmrServiceType)
        {
            if (!mmrServiceType.IsAssignableTo(typeof(MMRService)))
            {
                throw new ArgumentException($"Argument '{nameof(mmrServiceType)}' must be assignable to type '{nameof(MMRService)}'.");
            }
            _mmrServiceType = mmrServiceType;
        }

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var mmrService = (MMRService)services.GetService(_mmrServiceType);
            if (mmrService.IsReady())
            {
                return Task.FromResult(PreconditionResult.FromSuccess());
            }
            else
            {
                return Task.FromResult(PreconditionResult.FromError("This command is currently unavailable."));
            }
        }
    }
}
