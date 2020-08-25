using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CQRSAndEventSourcing.Infrastructure.Factories
{
    public class MongoDBFactory
    {
        private readonly IConfiguration _configuration;

        public MongoDBFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoDatabase GetDatabase()
        {
            var url = new MongoUrl(_configuration.GetConnectionString("Mongo"));
            var client = new MongoClient(url);
            var database = client.GetDatabase("HumanResources");

            var conventionPack = new ConventionPack {
                new IgnoreExtraElementsConvention(true)
            };

            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, x => true);

            return database;
        }
    }
}
