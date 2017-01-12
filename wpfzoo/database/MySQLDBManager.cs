using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;

namespace wpfzoo.database
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlConnectionFactory))]

    public class MySQLDBManager : DbContext
    {
        public DbSet<Animal> animalTable;

        public void insert(Animal animal)
        {
            this.animalTable.Add(animal);
            this.SaveChanges();        }

    }
}
