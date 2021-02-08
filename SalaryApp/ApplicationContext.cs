using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace SalaryApp
{
     class ApplicationContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Group_attribs> Group_attribs { get; set; }

        public DbSet<Inferrior> Inferriors { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
