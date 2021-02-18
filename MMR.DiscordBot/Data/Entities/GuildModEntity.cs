using ServiceStack.DataAnnotations;

namespace MMR.DiscordBot.Data.Entities
{
    [Alias("GuildMods")]
    public class GuildModEntity
    {
        [PrimaryKey, AutoIncrement]
        public ulong Id { get; set; }

        public ulong RoleId { get; set; }

        public ulong GuildId { get; set; }
    }
}
