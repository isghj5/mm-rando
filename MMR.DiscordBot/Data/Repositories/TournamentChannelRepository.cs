using System.Threading.Tasks;
using MMR.DiscordBot.Data.Entities;
using ServiceStack.OrmLite;

namespace MMR.DiscordBot.Data.Repositories
{
    public class TournamentChannelRepository : BaseRepository<TournamentChannelEntity>
    {
        public TournamentChannelRepository(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<bool> ExistsByChannelId(ulong channelId)
        {
            using (var db = ConnectionFactory.Open())
            {
                return await db.ExistsAsync<TournamentChannelEntity>(tc => tc.ChannelId == channelId);
            }
        }
    }
}
