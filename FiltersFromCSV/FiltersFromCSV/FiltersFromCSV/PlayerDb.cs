using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltersFromCSV
{
    public  class PlayerDB :DbContext
    {
        public DbSet<Player> Players { get; set; }
    }  
}
