using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Order.Data.Entities;
using Order.Data.Interfaces;
using Order.Data.MongoDBEntities;
using System.Text.Json;

namespace Order.Data.Repositories
{
    public class MongoDBOrderRepository : IOrderRepository
    {
        private readonly ILogger<MongoDBOrderRepository> _logger;
        private const string DATABASE_NAME = "orders_db";
        private const string COLLECTION_NAME = "orders";
        private Lazy<IMongoCollection<BsonDocument>> _lazyCollection;

        public MongoDBOrderRepository(string connectionString, ILogger<MongoDBOrderRepository> logger) 
        {
            _logger = logger;
            var client = new MongoClient(connectionString);
            _lazyCollection = new(client.GetDatabase(DATABASE_NAME).GetCollection<BsonDocument>(COLLECTION_NAME));
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            var oredersCollection = _lazyCollection.Value;
            var orders = oredersCollection.Find(_ => true).ToList();

            foreach (var order in orders)
            {
                yield return BsonSerializer.Deserialize<MongoDBOrderEntity>(order);
            }
        }

        public async Task<bool> InsertOrder(OrderEntity order)
        {
            var oredersCollection = _lazyCollection.Value;
            var bson = BsonDocument.Parse(JsonSerializer.Serialize(order));

            try
            {
                await oredersCollection.InsertOneAsync(bson);
                _logger.LogInformation("New order inserted successfully");
                return true;
            }catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }
    }
}
