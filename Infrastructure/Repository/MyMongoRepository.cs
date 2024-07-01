using api_reservas.Core.Models.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repository
{
    public class MyMongoRepository
    {
        public IMongoDatabase mongoDatabase;

        public MyMongoRepository(IOptions<DatabaseSettings> dabaseSettings)
        {
            var mongoClient = new MongoClient(dabaseSettings.Value.ConnectionString);

            mongoDatabase = mongoClient.GetDatabase(dabaseSettings.Value.DatabaseName);
        }
    }
}
