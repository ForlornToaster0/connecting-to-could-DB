using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{
    public class View
    {
        public static void Seeing(string Media, string Connect)
        {
            var client = new MongoClient(Connect);

            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Geek_Rating.Media");
            var builder = Builders<BsonDocument>.Filter;

            var test = Builders<BsonDocument>.Filter.And(Builders<BsonDocument>.Filter.Eq("Media", Media),
                builder.Exists("SlysPoints")
                );
            var doc = movie.Find( test).Project("{Media: 0, _id:0}").ToList();
            char[] Trimming = { '{', '}', ',' };
            doc.TrimExcess();
            foreach (var item in doc)
            {
                Console.WriteLine(item.ToString().Trim(Trimming));
            }
        }
    }
}
