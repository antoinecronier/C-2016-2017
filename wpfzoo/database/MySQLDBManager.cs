using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;

namespace wpfzoo.database
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

    public class MySQLDBManager : DbContext
    {
        public DbSet<Animal> AnimalTable { get; set; };

        public MySQLDBManager() : base("Server=localhost;Port=3306;Database=zoo;Uid=root;Pwd=''")
        {

        }

        public void insert(Animal animal)
        {
            this.animalTable.Add(animal);
            this.SaveChanges();
        }
    }
}
