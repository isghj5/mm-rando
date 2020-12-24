using System.Collections.Generic;
using System.Threading.Tasks;
using MMR.DiscordBot.Data.Entities;
using ServiceStack.OrmLite;

namespace MMR.DiscordBot.Data.Repositories
{
    public class GuildModRepository : BaseRepository<GuildModEntity>
    {
        public GuildModRepository(ConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IList<GuildModEntity>> ListByGuildId(ulong guildId)
        {
            using (var db = ConnectionFactory.Open())
            {
                return await db.SelectAsync<GuildModEntity>(gm => gm.GuildId == guildId);
            }
        }
    }
}
