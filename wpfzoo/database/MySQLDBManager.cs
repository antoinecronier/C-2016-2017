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
    public class MySQLDBManager<T> : DbContext where T : class
    {
        //public DbSet<Animal> AnimalTable { get; set; }
        public DbSet<T> DbSetT { get; set; }
        //public DbSet<StreetNumber> StreetNumberTable { get; set; }
        //public DbSet<Job> JobTable { set; get; }
        //public DbSet<Address> AddressTable { set; get; }
        //public DbSet<Schedule> ScheduleTable { set; get; }
        //public DbSet<Structure> StructureTable { set; get; }
        //public DbSet<Zoo> ZooTable { set; get; }


        public MySQLDBManager() : base("Server=127.0.0.1;Port=3306;Database=zoo;Uid=root;Pwd=''")
        {
            this.SaveChanges();
        }

        public void insert(T item)
        {
            this.DbSetT.Add(item);
            this.SaveChanges();
        }
    }

}
