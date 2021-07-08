using System;
using ServiceStack.DataAnnotations;

namespace MMR.DiscordBot.Data.Entities
{
    [Alias("UserSeeds")]
    public class UserSeedEntity
    {
        [PrimaryKey]
        public ulong UserId { get; set; }

        public DateTime LastSeedRequest { get; set; }

        public string Version { get; set; }
    }
}
