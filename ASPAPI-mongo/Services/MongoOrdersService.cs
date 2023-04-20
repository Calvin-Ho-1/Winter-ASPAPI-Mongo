using ASPAPI_mongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ASPAPI_mongo.Services
{
    public class MongoOrdersService
    {
        private readonly IMongoCollection<MongoOrder> _ordersCollection;
        public MongoOrdersService(
        IOptions<OrderDatabaseSettings> orderDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            orderDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
            orderDatabaseSettings.Value.DatabaseName);
            _ordersCollection = mongoDatabase.GetCollection<MongoOrder>(
            orderDatabaseSettings.Value.OrdersCollectionName);
        }

        public async Task<List<MongoOrder>> GetAsync() =>
        await _ordersCollection.Find(_ => true).ToListAsync();
        public async Task<MongoOrder?> GetAsync(string id) =>
        await _ordersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(MongoOrder newOrder) =>
        await _ordersCollection.InsertOneAsync(newOrder);
        public async Task UpdateAsync(string id, MongoOrder updatedOrder) =>
        await _ordersCollection.ReplaceOneAsync(x => x.Id == id, updatedOrder);
        public async Task RemoveAsync(string id) =>
        await _ordersCollection.DeleteOneAsync(x => x.Id == id);


    }
}
