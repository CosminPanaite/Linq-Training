using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace FiltersFromCSV
{
    public class Player
    {   public int Id { get; set; }   
        public string Name { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public int Height { get; set; }

        public int Weight { get; set; }

        public float Age { get; set; }
        internal static Player ParseFromCSV(string line)
        {
            var columns = line.Split(',');
            return new Player
            {
                Name = columns[0],
                Team = columns[1],
                Position = columns[2],
                Height = int.Parse(columns[3]),
                Weight = int.Parse(columns[4]),
                Age = float.Parse(columns[5])
            };
    }
    }
}
