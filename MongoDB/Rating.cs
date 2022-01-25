using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB
{
    public class Rating
    {

        public static void Rate(string Connect, string User)
        {
            var client = new MongoClient(Connect);


            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Media");
            int docNum = Find(Connect, "true", "Rating", User);

            var FilterArray = Builders<BsonDocument>.Filter.Exists(User);
            var doc = movie.Find(!FilterArray).Project("{Media: 0, _id:0," + User + ":0}").ToList();

            Console.WriteLine("What's it rating?");

            string input2 = Console.ReadLine();
            if (int.TryParse(input2, out int rating))
            {

                Console.WriteLine("when did you watch it? YYYY-MM-DD");
                string date = Console.ReadLine() + "T00:00:00.000+00:00";
                if (date != null)
                {
                    BsonDateTime date2 = new BsonDateTime(DateTime.Parse(date));
                    var Update = Builders<BsonDocument>.Update.Push(User, new BsonDocument { { "Points", rating }, { "Watched", date2 }, { "User", User } });



                    movie.UpdateOne(doc[docNum], Update);
                }

            }

        }
        public static int Find(string Connect, string Media, string Using, string User)
        {
            var client = new MongoClient(Connect);


            IMongoDatabase db = client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Media");


            var doc = movie.Find(new BsonDocument()).ToList();
            switch (Using)
            {
                case "Rating":
                    {
                        var FilterArray = Builders<BsonDocument>.Filter.Exists(User);
                        doc = movie.Find(!FilterArray).Project("{Media: 0, _id:0," + User + ": 0}").ToList();

                        break;
                    }
                case "RemoveRating":

                case "View":
                    {
                        var FilterArray = Builders<BsonDocument>.Filter.And(Builders<BsonDocument>.Filter.Eq("Media", Media),
                   Builders<BsonDocument>.Filter.Exists(User)
                   );

                        if (Media == "A")
                        {
                            FilterArray = Builders<BsonDocument>.Filter.And(Builders<BsonDocument>.Filter.Gt("Media", Media),
             Builders<BsonDocument>.Filter.Exists(User));

                        }

                        doc = movie.Find(FilterArray).ToList();
                        int i = 0;
                        foreach (var item in doc)
                        {
                            string rating = item[4].ToString().Substring(14, 2);
                            if (rating.Contains(','))
                            {
                                rating = item[4].ToString().Substring(14, 1);
                            }
                            i++;
                            string date = item[4].ToString().Substring(38, 10);
                            Console.Write($"({i})Title: {item[1]} Released: {item[3].ToString().Substring(0, 10)}");

                            Console.WriteLine($"                Points: {rating} Watched: {date}");



                        }
                        break;
                    }
                case "Remove":
                    {
                        doc = movie.Find(new BsonDocument()).Project("{Media: 0, _id:0}").ToList();

                        break;
                    }
                default:
                    { break; }
            }


            if (Using == "Rating" || Using == "Remove")
            {

                int i = 0;
                foreach (var item in doc)
                {

                    i++;
                    Console.WriteLine($"({i}) Title: {item[0]} Release : {item[1].ToString().Substring(0, 10)}");

                }
            }

            if (Using == "Rating" || Using == "Remove" || Using == "RemoveRating")
            {
                string Input = Console.ReadLine();
                if (int.TryParse(Input, out int docNum))
                {
                    docNum = int.Parse(Input) - 1;
                }
                return docNum;

               
            }
            return 0;
        }
    }
}

