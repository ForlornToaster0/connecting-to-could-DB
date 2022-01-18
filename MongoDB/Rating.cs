using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB
{
    public class Rating
    {

        public static void Rate(string Connect)
        {
            int found = Find(Connect);
            var client = new MongoClient(Connect);


            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Geek_Rating.Media");
            int docNum= Find(Connect);
            Console.WriteLine("What do you want to rate?");
            var builder = Builders<BsonDocument>.Filter;
            var FilterArray = builder.Exists("SlysPoints");
            var doc = movie.Find(!FilterArray).Project("{Media: 0, _id:0,SlysPoints:0}").ToList();
            int i = 0;
            char[] Trimming = { '{', '}' };

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
        static int Find(string Connect)
        {
            var client = new MongoClient(Connect);


            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Geek_Rating.Media");

            Console.WriteLine("What do you want to rate?");
            var FilterArray = Builders<BsonDocument>.Filter.Exists("SlysPoints");
            var doc = movie.Find(!FilterArray).Project("{Media: 0, _id:0,SlysPoints:0}").ToList();
            int i = 0;
            int docNum = 0;
            char[] Trimming = { '{', '}' };
            foreach (var item in doc)
            {

                i++;
                Console.WriteLine($"({i})   {item.ToString().Trim(Trimming)}");
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

