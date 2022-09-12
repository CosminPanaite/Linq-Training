using System.Data.Entity;
using System.Xml.Linq;

namespace FiltersFromCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = TakeElementsFromCSV("C:\\Users\\Panaite Cosmin\\source\\repos\\FiltersFromCSV\\FiltersFromCSV\\players.csv");
            foreach (var player in players)
            {
                Console.WriteLine(player.Name);
            }
            Program.printVeterans();
            Program.printSmallest();
            Program.anyPlayerMoreThan20();
            Program.allPlayersMoreThan23();
            Program.printAllAdamsWithEnumerator();
            Program.computeSquareWithFunc();

            var document = new XDocument();
            var players2 = new XElement("Players");
            foreach (var record in players)
            {
                var player = new XElement("Player");
                var name = new XAttribute("Name", record.Name);
                var team = new XAttribute("Team", record.Team);
                var position = new XAttribute("Position", record.Position);
                var height = new XAttribute("Height", record.Height.ToString());
                var weight = new XAttribute("Weight", record.Weight.ToString());
                var age = new XAttribute("Age", record.Age.ToString());
                player.Add(name);
                player.Add(team);
                player.Add(position);
                player.Add(height);
                player.Add(weight);
                player.Add(age);

                players2.Add(player);


            }
            document.Add(players2);
            document.Save("players2.xml");

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PlayerDB>());
            InsertData();
            
        }

        private static void InsertData()
        {
            var players = TakeElementsFromCSV("C:\\Users\\Panaite Cosmin\\source\\repos\\FiltersFromCSV\\FiltersFromCSV\\players.csv");
            var db = new PlayerDB();
            if (!db.Players.Any())
            {
                foreach (var player in players)
                {
                    db.Players.Add(player);
                }
                db.SaveChanges();
            }
        }

        private static List<Player> TakeElementsFromCSV(string path)
        {
            var query =
                from line in File.ReadAllLines(path).Skip(1)
                where line.Length > 1
                select Player.ParseFromCSV(line);

            return query.ToList();

        }
        private static void printVeterans()
        {
            var players = TakeElementsFromCSV("C:\\Users\\Panaite Cosmin\\source\\repos\\FiltersFromCSV\\FiltersFromCSV\\players.csv");
            var query = players.OrderByDescending(p => p.Age)
                               .ThenBy(p => p.Name);
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Filter 2 method:");

            foreach (var player in query.Take(7))
            {
                Console.WriteLine($"{player.Name} : {player.Age}");
            }
        }
        private static void printSmallest()
        {
            var players = TakeElementsFromCSV("C:\\Users\\Panaite Cosmin\\source\\repos\\FiltersFromCSV\\FiltersFromCSV\\players.csv");
            var query = players.OrderBy(p => p.Height)
                               .ThenBy(p => p.Name);
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Filter 3 method:");

            foreach (var player in query.Take(7))
            {
                Console.WriteLine($"{player.Name} : {player.Height}");
            }
        }
        private static void anyPlayerMoreThan20()
        {
            var players = TakeElementsFromCSV("C:\\Users\\Panaite Cosmin\\source\\repos\\FiltersFromCSV\\FiltersFromCSV\\players.csv");
            var result = players.Any(p => p.Age > 20.0);

            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Filter 4 method:");


            Console.WriteLine("Is the any player older than 20 ? The answer is : " + result);

        }
        private static void allPlayersMoreThan23()
        {
            var players = TakeElementsFromCSV("C:\\Users\\Panaite Cosmin\\source\\repos\\FiltersFromCSV\\FiltersFromCSV\\players.csv");
            var result = players.All(p => p.Age > 23.0);

            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Filter 5 method:");


            Console.WriteLine("Are all the players older than 23 ? The answer is : " + result);

        }
        private static void printAllAdamsWithEnumerator()
        {
            var players = TakeElementsFromCSV("C:\\Users\\Panaite Cosmin\\source\\repos\\FiltersFromCSV\\FiltersFromCSV\\players.csv");
            var result = players.Where(p => p.Name.Contains("Adam"));
            var enumerator = result.GetEnumerator();
            var resultList = new List<string>();
            while (enumerator.MoveNext())
            {
                resultList.Add(enumerator.Current.Name);

            }
            Console.WriteLine();
            Console.WriteLine("Filter 6 method:");
            Thread.Sleep(1000);

            foreach (var item in resultList)
            {
                Console.WriteLine(item);

            }
        }


        private static void computeSquareWithFunc()
        {
            var players = TakeElementsFromCSV("C:\\Users\\Panaite Cosmin\\source\\repos\\FiltersFromCSV\\FiltersFromCSV\\players.csv");
            var result = players.First();
            Func<float, float> square = x => x * x;

            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("Filter 7 method:");
            Console.WriteLine(square(result.Age));


        }
    }



}