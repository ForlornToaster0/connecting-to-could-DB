using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB
{
    public class Adding 
    {
        MongoClient Client { get; set; }
        string Media { get; set; }
        public Adding(MongoClient client, string media)
        {
            this.Client = client;
            this.Media = media;
        }

        public void Added()
        {
            string Title;
            string Date;

          
            IMongoDatabase db = Client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Media");

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

