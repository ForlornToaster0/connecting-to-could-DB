using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB;

namespace MongoDB
{
    public class Testing
    {

        public static void remove(string Connect, string User)
        {
          
            var client = new MongoClient(Connect);


            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Media");


            var doc = movie.Find(new BsonDocument()).ToList();
            
            
        }

    }
}
