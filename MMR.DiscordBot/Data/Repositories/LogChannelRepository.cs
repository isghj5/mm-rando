using MMR.DiscordBot.Data.Entities;

namespace MMR.DiscordBot.Data.Repositories
{
    public class LogChannelRepository : BaseRepository<LogChannelEntity>
    {
        public LogChannelRepository(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
