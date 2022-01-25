using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB
{
    public class Adding
    {

        public static void Media(string Media, string Connect)
        {
            string Title;
            string Date;

            var client = new MongoClient(Connect);

            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

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
        public static void remove(string Connect, string Using, string User)
        {

            var client = new MongoClient(Connect);


            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Media");


            var doc = movie.Find(new BsonDocument()).ToList();
            switch (Using)
            {
                case "Remove":
                    {

                        int mediaNum = Rating.Find(Connect, "A", Using, User);

                        movie.DeleteOne(doc[mediaNum]);

                        break;

                    }
                case "RemoveRating":
                    {

                        var FilterArray = Builders<BsonDocument>.Filter.Exists(User);
                        doc = movie.Find(FilterArray).ToList();
                        int mediaNum = Rating.Find(Connect, "A", Using, User);

                        var Update = Builders<BsonDocument>.Update.Unset(User);

                        movie.UpdateOne(doc[mediaNum], Update);

                        break;
                    }


            }
        }
    }
}
