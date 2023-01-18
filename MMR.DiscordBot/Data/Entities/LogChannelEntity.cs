using ServiceStack.DataAnnotations;

namespace MMR.DiscordBot.Data.Entities
{
    [Alias("LogChannels")]
    public class LogChannelEntity
    {
        [PrimaryKey]
        public ulong ChannelId { get; set; }
    }
}
