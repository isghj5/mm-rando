using MMR.DiscordBot.Data.Entities;
using ServiceStack.OrmLite;
using System;

namespace MMR.DiscordBot.Data
{
    public class ConnectionFactory : OrmLiteConnectionFactory
    {
        public ConnectionFactory() : base("seeds.db", SqliteDialect.Provider)
        {
            DialectProvider.GetDateTimeConverter().DateStyle = DateTimeKind.Utc;

            using (var db = this.Open())
            {
                if (!db.TableExists<UserSeedEntity>())
                {
                    db.CreateTable<UserSeedEntity>();
                }
                if (!db.ColumnExists<UserSeedEntity>(x => x.Version))
                {
                    db.AddColumn<UserSeedEntity>(x => x.Version);
                }
                db.CreateTableIfNotExists<GuildModEntity>();
            }
        }
    }
}
