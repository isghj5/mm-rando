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
            if (!mmrServiceType.IsAssignableTo(typeof(MMRBaseService)))
            {
                throw new ArgumentException($"Argument '{nameof(mmrServiceType)}' must be assignable to type '{nameof(MMRBaseService)}'.");
            }
            _mmrServiceType = mmrServiceType;
        }

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var mmrService = (MMRBaseService)services.GetService(_mmrServiceType);
            if (mmrService.IsReady())
            {
                return Task.FromResult(PreconditionResult.FromSuccess());
            }
            else
            {
                return Task.FromResult(PreconditionResult.FromError(ExecuteResult.FromError(CommandError.UnknownCommand, "This command is currently unavailable.")));
            }
        }
    }
}
