using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB
{
    public class Rating
    {

        public static void Rate(string Connect)
        {
            var client = new MongoClient(Connect);


            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Geek_Rating.Media");
            int docNum = Find(Connect, "true", "Rating");
            Console.WriteLine("What do you want to rate?");
            var builder = Builders<BsonDocument>.Filter;
            var FilterArray = builder.Exists("SlysPoints");
            var doc = movie.Find(!FilterArray).Project("{Media: 0, _id:0,SlysPoints:0}").ToList();
            int i = 0;

            Console.WriteLine("What's it rating?");

            string input2 = Console.ReadLine();
            if (input2 != null)
            {
                int rating = int.Parse(input2);

                Console.WriteLine("when did you watch it? YYYY-MM-DD");
                string date = Console.ReadLine();
                if (date != null)
                {
                    var Update = Builders<BsonDocument>.Update.Push("SlysPoints", new BsonDocument { { "Points", rating }, { "Watched", date } });



                    movie.UpdateOne(doc[docNum], Update);
                }

            }

        }
        public static int Find(string Connect, string Media, string Using)
        {
            var client = new MongoClient(Connect);


            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Geek_Rating.Media");

            var FilterArray = Builders<BsonDocument>.Filter;
            var test = Builders<BsonDocument>.Filter.And();
            var doc = movie.Find(new BsonDocument()).ToList();
            switch (Using)
            {
                case "Rating":
                    {
                        test = FilterArray.Exists("SlysPoints");
                        doc = movie.Find(!test).Project("{Media: 0, _id:0,SlysPoints:0}").ToList();

                        break;
                    }
                case "View":
                    {
                        test = Builders<BsonDocument>.Filter.And(Builders<BsonDocument>.Filter.Eq("Media", Media),
              Builders<BsonDocument>.Filter.Exists("SlysPoints")
              );
                        doc = movie.Find(test).Project("{Media: 0, _id: 0}").ToList();

                        break;
                    }
                default:
                    { break; }
            }


            int i = 0;
            int docNum = 0;
            char[] Trimming = { '{', '}' };
            foreach (var item in doc)
            {

                i++;
                Console.WriteLine($"({i}) {item.ToString().Trim(Trimming)}");
            }
            string Input = Console.ReadLine();
            if (Input != null)
            {
                docNum = int.Parse(Input) - 1;
            }
            Console.WriteLine(docNum);
            return docNum;
        }
    }
}

