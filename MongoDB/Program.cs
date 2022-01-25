using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
while (true)
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("Username");
    string Username = Console.ReadLine();
    Console.WriteLine("Password");
    string Password = Console.ReadLine();
    string connect = $"mongodb+srv://{Username}:{Password}@slyandsam.v6vet.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";

    var client = new MongoClient(
    connect);

    var database = client.GetDatabase("myFirstDatabase");

    bool isAlive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(2000);

    if (isAlive)
    {
        Console.Clear();
        while (true)
        {

            Console.WriteLine("(1)Add Media\n(2)Rate Media\n(3)View\n(4)Remove");
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
                        Rating.Rate(connect, Username);
                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("What Kind?\n(1)Game\n(2)Show\n(3)Movie\n(4)All");
                        string Input = Console.ReadLine();
                        switch (Input)
                        {
                            case "1":
                                {
                                    Rating.Find(connect, "G", "View", Username);
                                    break;
                                }
                            case "2":
                                {
                                    Rating.Find(connect, "S", "View", Username);
                                    break;
                                }
                            case "3":
                                {
                                    Rating.Find(connect, "M", "View", Username);
                                    break;
                                }
                            case "4":
                                {

                                    Rating.Find(connect, "A", "View", Username);
                                    break;
                                }
                            default:
                                {

                                    break;
                                }
                        }
                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("Remove\n(1) Media\n(2) Rating");
                        string Input = Console.ReadLine();
                        switch (Input)
                        {
                            case "1":
                                {
                                    Adding.remove(connect, "Remove", Username);

                                    break;
                                }
                                case"2":
                                {
                                    Adding.remove(connect, "RemoveRating", Username);

                                    break;
                                }

                        }
                        break;
                    }
                case "T":
                    {
                        Testing.remove(connect, Username);
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

