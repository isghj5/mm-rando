using System;
using System.IO;

namespace MMR.DiscordBot.Services
{
    public class MMRBetaService : MMRService
    {
        private const string MMR_CLI = "MMRBETA_CLI";

        public MMRBetaService() : base()
        {
            _cliPath = Environment.GetEnvironmentVariable(MMR_CLI);
            if (string.IsNullOrWhiteSpace(_cliPath))
            {
                _cliPath = null;
            }
            else if (!Directory.Exists(_cliPath))
            {
                _cliPath = null;
            }
        }
    }
}
