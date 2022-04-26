using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using nrmcontrolextension.DbContexts;
using nrmcontrolextension.Models;

namespace BibliotecaDLL.DbContexts
{
    public class MongoDbContext
    {
        public static bool IsSSL { get; set; }

        private IMongoDatabase Database { get; }

        public MongoDbContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(Connection.ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                Database = mongoClient.GetDatabase(Connection.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return Database.GetCollection<User>("User");
            }
        }

    }
}