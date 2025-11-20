using MongoDB.Driver;

namespace LW4_API.MongoDB
{
   

    public class MongoDBClient
    {
        private static IMongoDatabase _db;
        private static MongoDBClient _instance;

        public static MongoDBClient Instance
        {
            get => _instance ?? new MongoDBClient();
        }

        private MongoDBClient()
        {
            var connectionString = "mongodb+srv://nazarshejkin_db_user:eXsanO17aQW49eV6@nazar.pjrmold.mongodb.net/?appName=Nazar"; 
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase("mydatabase");

        }
        public static async Task<int> GetNextId(string collectionName)
        {
            var counters = _db.GetCollection<Counter>("counters");

            var update = Builders<Counter>.Update.Inc(c => c.Value, 1);
            var options = new FindOneAndUpdateOptions<Counter>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var result = await counters.FindOneAndUpdateAsync(
                c => c.Id == collectionName,
                update,
                options
            );

            return result.Value;
        }

        public IMongoCollection<T> GetCollection<T>(string name) => _db.GetCollection<T>(name);
    }
    public class Counter
    {
        public string Id { get; set; } // Назва колекції
        public int Value { get; set; }
       
    }
}
