using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace MMR.DiscordBot.Data.Repositories
{
    public abstract class BaseRepository<TEntity>
    {
        protected ConnectionFactory ConnectionFactory { get; }

        public BaseRepository(ConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public async Task<TEntity> GetById(ulong id)
        {
            using (var db = ConnectionFactory.Open())
            {
                return await db.SingleByIdAsync<TEntity>(id);
            }
        }

        public async Task Save(TEntity entity)
        {
            using (var db = ConnectionFactory.Open())
            {
                await db.SaveAsync(entity);
            }
        }

        public async Task DeleteById(ulong id)
        {
            using (var db = ConnectionFactory.Open())
            {
                await db.DeleteByIdAsync<TEntity>(id);
            }
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            using (var db = ConnectionFactory.Open())
            {
                return await db.ExistsAsync(predicate);
            }
        }

        public async Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate)
        {
            using (var db = ConnectionFactory.Open())
            {
                return await db.SingleAsync(predicate);
            }
        }
    }
}
