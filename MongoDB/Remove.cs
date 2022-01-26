using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{
    public class Remove : Search
    {

        int DocNum { get; set; }
        public Remove(MongoClient client, string media, string purpose, string username, int docNum) : base(client, media, purpose, username) //connect, "A", "View", Username
        {

            this.DocNum = docNum;
        }
        public void Removed(MongoClient client, string purpose)
        {

            
            IMongoDatabase db = Client.GetDatabase("myFirstDatabase");

            var movie = db.GetCollection<BsonDocument>("Media");

            var doc = movie.Find(new BsonDocument()).ToList();
            switch (purpose)
            {
                case "Remove":
                    {

                        movie.DeleteOne(doc[DocNum]);

                        break;

                    }
                case "RemoveRating":
                    {

                        var FilterArray = Builders<BsonDocument>.Filter.Exists(purpose);
                        doc = movie.Find(FilterArray).ToList();

                        var Update = Builders<BsonDocument>.Update.Unset(purpose);

                        movie.UpdateOne(doc[DocNum], Update);

                        break;
                    }
            }
        }
    }
}
