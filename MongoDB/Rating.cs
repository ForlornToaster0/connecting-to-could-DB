using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB
{
    public class Rating : Search
    {
        int DocNum { get; set; }
        public Rating(MongoClient client, string media, string purpose, string username, int docNum) 
            : base(client, media, purpose, username) //connect, "A", "View", Username
        {
            this.DocNum = docNum;
        }
        public void Rate(MongoClient client, string username)
        {
            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Media");
            var FilterArray = Builders<BsonDocument>.Filter.Exists(username);
            var doc = movie.Find(!FilterArray).Project("{Media: 0, _id:0," + username + ":0}").ToList();

            Console.WriteLine("What's it rating?");

            string input2 = Console.ReadLine();
            if (int.TryParse(input2, out int rating))
            {
                Console.WriteLine("when did you watch it? YYYY-MM-DD");
                string date = Console.ReadLine() + "T00:00:00.000+00:00";

                if (date != null)
                {

                    BsonDateTime date2 = new BsonDateTime(DateTime.Parse(date));
                    var Update = Builders<BsonDocument>.Update.Push(username, new BsonDocument { { "Points", rating }, { "Watched", date2 }, { "User", username } });

                    movie.UpdateOne(doc[DocNum], Update);
                }

            }

        }
    }
}

