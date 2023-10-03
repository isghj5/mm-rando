using ServiceStack.DataAnnotations;
using System;

namespace MMR.DiscordBot.Data.Entities
{
    public class TournamentSeedEntity
    {
        [PrimaryKey]
        public ulong UserId { get; set; }

        public DateTime DateTime { get; set; }

        public string Version { get; set; }
    }
}
