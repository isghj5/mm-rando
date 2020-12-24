using MMR.DiscordBot.Data.Entities;

namespace MMR.DiscordBot.Data.Repositories
{
    public class UserSeedRepository : BaseRepository<UserSeedEntity>
    {
        public UserSeedRepository(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
