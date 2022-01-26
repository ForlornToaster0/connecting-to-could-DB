using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{
    public class Search
    {
       public string Purpose { get; set; }
       public string Media { get; set; }
       public MongoClient Client { get; set; }
       public string UserName { get; set; }
        public Search(MongoClient client, string media, string purpose, string username)   //connect, "A", "View", Username
        {
            this.Client = client;
            this.UserName = username;
            this.Media = media;
            this.Purpose = purpose;
        }
        public List<BsonDocument> Find()
        {
            IMongoDatabase db = Client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Media");

            var doc = movie.Find(new BsonDocument()).ToList();

            switch (Purpose)
            {
                case "Rating":
                    {
                        var FilterArray = Builders<BsonDocument>.Filter.Exists(UserName);
                        doc = movie.Find(!FilterArray).Project("{Media: 0, _id:0," + UserName + ": 0}").ToList();

                        break;
                    }
                case "RemoveRating":
                case "View":
                    {
                        var FilterArray = Builders<BsonDocument>.Filter.And(Builders<BsonDocument>.Filter.Eq("Media", Media),
                                                                            Builders<BsonDocument>.Filter.Exists(UserName));
                        if (Media == "A")
                        {
                            FilterArray = Builders<BsonDocument>.Filter.And(Builders<BsonDocument>.Filter.Gt("Media", Media),
                                                                            Builders<BsonDocument>.Filter.Exists(UserName));
                        }

                        doc = movie.Find(FilterArray).ToList();
                        return doc;
                        break;
                    }
                case "Remove":
                    {
                        doc = movie.Find(new BsonDocument()).Project("{Media: 0, _id:0}").ToList();
                        return doc;
                        break;
                    }
                default:
                    { break; }
            }
            return null;
        }
        public int docNums(List<BsonDocument> doc)
        {
            int i = 0;
            foreach (var item in doc)
            {
                if (Purpose == "Rating" || Purpose == "Remove")
                {
                    i++;
                    Console.WriteLine($"({i}) Title: {item[0]} Release : {item[1].ToString().Substring(0, 10)}");

                }
                else
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


            }
            if (Purpose == "Rating" || Purpose == "Remove" || Purpose == "RemoveRating")
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


