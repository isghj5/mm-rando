using ServiceStack.DataAnnotations;

namespace MMR.DiscordBot.Data.Entities
{
    [Alias("TournamentChannels")]
    public class TournamentChannelEntity
    {
        [PrimaryKey, AutoIncrement]
        public ulong Id { get; set; }

        public ulong ChannelId { get; set; }

        //public ulong GuildId { get; set; }
    }
}
