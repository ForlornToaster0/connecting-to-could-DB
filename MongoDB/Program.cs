using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
while (true)
{
    Console.WriteLine("Username");
    string Username = Console.ReadLine();
    Console.WriteLine("Password");
    string Password = Console.ReadLine();
    string connect = $"mongodb+srv://{Username}:{Password}@slyandsam.v6vet.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";

    var client = new MongoClient(
    connect);
    IMongoDatabase db = client.GetDatabase("myFirstDatabase");

    var movie = db.GetCollection<BsonDocument>("Geek_Rating.Media");
    var database = client.GetDatabase("myFirstDatabase");

    bool isAlive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);

    if (isAlive)
    {
        Console.Clear();
        while (true)
        {

            Console.WriteLine("(1)Add Media\n(2)Rate Media\n(3)View");
            string Inout = Console.ReadLine().ToUpper();
            switch (Inout)
            {
                case "1":
                    {

                        Console.WriteLine("What Kind?\n(1)Game\n(2)Show\n(3)Movie");
                        string Input = Console.ReadLine().ToUpper();
                        switch (Input)
                        {
                            case "1":
                                {
                                    Adding.Media("G", connect);
                                    break;
                                }
                            case "2":
                                {
                                    Adding.Media("S", connect);
                                    break;
                                }
                            case "3":
                                {
                                    Adding.Media("M", connect);
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                case "2":
                    {
                        Rating.Rate(connect);
                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("What Kind?\n(1)Game\n(2)Show\n(3)Movie");
                        string Input = Console.ReadLine().ToUpper();
                        switch (Input)
                        {
                            case "1":
                                {
                                    Rating.Find(connect, "G", "View");
                                    break;
                                }
                            case "2":
                                {
                                    Rating.Find(connect, "S", "View");
                                    break;
                                }
                            case "3":
                                {
                                    Rating.Find(connect, "M", "View");
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    Console.Clear();
}

