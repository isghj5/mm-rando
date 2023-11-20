using MMR.DiscordBot.Data.Entities;

namespace MMR.DiscordBot.Data.Repositories
{
    public class TournamentSeedRepository : BaseRepository<TournamentSeedEntity>
    {
        public TournamentSeedRepository(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}
