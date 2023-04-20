using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ASPAPI_mongo.Models
{
    public class MongoOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        //[BsonElement("title")]
        public string customerName { get; set; } = null!;
        public int phoneNumber { get; set; }
        public string productName { get; set; } = null!;
        public string productDesc { get; set; } = null!;
        public int productRating { get; set; } 
        public string transactionType { get; set; } = null!;

    }
}
