using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Order.Data.Entities;

namespace Order.Data.MongoDBEntities
{
    internal class MongoDBOrderEntity : OrderEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
