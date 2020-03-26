using MMR.DiscordBot.Data.Entities;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.DiscordBot.Data
{
    public class ConnectionFactory : OrmLiteConnectionFactory
    {
        public ConnectionFactory() : base("seeds.db", SqliteDialect.Provider)
        {
            DialectProvider.GetDateTimeConverter().DateStyle = DateTimeKind.Utc;

            using (var db = this.Open())
            {
                db.CreateTableIfNotExists<UserSeedEntity>();
            }
        }
    }
}
