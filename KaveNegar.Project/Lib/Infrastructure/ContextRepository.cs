using KaveNegar.Project.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaveNegar.Infrastructure
{
    public class ContextRepository : DbContext
    {
        public ContextRepository() : base("name=KaveNegarContext")
        {

        }

        public DbSet<Numbers> Numbers { get; set; }
    }
}
