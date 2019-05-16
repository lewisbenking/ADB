using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    class MongoInteraction
    {
        public void MongoDBInteraction()
        {
            Task t = MainTask();
            t.Wait();
        }

        private static async Task MainTask()
        {
            MongoClient mongoClient = new MongoClient("mongodb+srv://kingl:REPLACEWITHPASSWORD@adb-lk-cluster-no3pl.mongodb.net/test?retryWrites=true");
            IMongoDatabase mongoDataBase = mongoClient.GetDatabase("HistoryDB");
            var collection = mongoDataBase.GetCollection<BsonDocument>("");
            var filter = new BsonDocument();
            var results = await collection.Find(filter).ToListAsync();
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }
    }
}
