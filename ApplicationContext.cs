using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SeCongratulator.Models;

namespace SeCongratulator
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Congratulation> Congratulations { get; set; }
    }
}
