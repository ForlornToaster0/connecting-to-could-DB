using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

Console.WriteLine("Username");
string Username = Console.ReadLine();
Console.WriteLine("Password");
string Password = Console.ReadLine();
string connect = $"mongodb+srv://{Username}:{Password}@slyandsam.v6vet.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
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
                            View.Seeing("G", connect);
                            break;
                        }
                    case "2":
                        {
                            View.Seeing("S", connect);
                            break;
                        }
                    case "3":
                        {
                            View.Seeing("M", connect);
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


