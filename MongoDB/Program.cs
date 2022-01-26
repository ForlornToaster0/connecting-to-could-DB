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
                                    Adding adding = new(client, "G");
                                    adding.Added();
                                    break;
                                }
                            case "2":
                                {
                                    Adding adding = new(client, "S");
                                    adding.Added();
                                    break;
                                }
                            case "3":
                                {
                                    Adding adding = new(client, "M");
                                    adding.Added();
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

                        Search search = new(client, "A", "Rating", Username);
                        List<BsonDocument> docList = search.Find();
                        int docnum = search.docNums(docList);
                        Rating rating = new(null, null, null, null, docnum);
                        rating.Rate(client,Username);
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
                                    Search search = new Search(client, "G", "View", Username);
                                    List<BsonDocument> docList = search.Find();
                                    int docnum = search.docNums(docList);
                                    break;
                                }
                            case "2":
                                {
                                    Search search = new Search(client, "S", "View", Username);
                                    List<BsonDocument> docList = search.Find();
                                    int docnum = search.docNums(docList);
                                    break;
                                }
                            case "3":
                                {
                                    Search search = new Search(client, "M", "View", Username);
                                    List<BsonDocument> docList = search.Find();
                                    int docnum = search.docNums(docList);
                                    break;
                                }
                            case "4":
                                {

                                    Search search = new Search(client, "A", "View", Username);
                                    List<BsonDocument> docList = search.Find();
                                    int docnum = search.docNums(docList);
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

                                    break;
                                }
                            case "2":
                                {

                                    break;
                                }

                        }
                        break;
                    }
                case "T":
                    {

                        Search search = new Search(client, "A", "Remove", Username);
                        List<BsonDocument> docList = search.Find();
                        int docnum = search.docNums(docList);

                        Remove remove = new Remove(null, null,null, null, docnum);
                        remove.Removed(client, Username);
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

