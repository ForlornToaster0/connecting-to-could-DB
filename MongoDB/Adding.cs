using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{
    public class Adding
    {

        public static void Media(string Media, string Connect)
        {
            string Title;
            string Date;


            var client = new MongoClient(
Connect);


            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Geek_Rating.Media");



            Console.WriteLine("Full title?");


            Title = Console.ReadLine();
            if (Title != null)
            {
                Console.WriteLine("What's it Release Date? YYYY-MM-DD");
                Date = Console.ReadLine();
                if (Date != null)
                {
                    var doc = new BsonDocument
                                {
                                    {"Title", Title },
                                    {"Release_Date", Date },
                                    {"Media", Media }
                                };
                    movie.InsertOne(doc);
                }
            }
        }
    }
}

