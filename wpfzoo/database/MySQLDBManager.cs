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
        public DbSet<T> DbSetT { get; set; }

        public MySQLDBManager() : base("Server=127.0.0.1;Port=3306;Database=zoo;Uid=root;Pwd=''")
        {
            InitTables init = new InitTables();
            this.SaveChanges();
        }

        public void initTables()
        {

        }

        public void insert(T item)
        {
            if (item != null)
            {
                this.DbSetT.Add(item);
                this.SaveChanges();
            }
        }

        public void delete(T item)
        {
            if (item != null)
            {
                this.DbSetT.Attach(item);
                this.DbSetT.Remove(item);
                this.SaveChanges();
            }
        }

        public void update(T item)
        {
            if (item != null)
            {
                this.DbSetT.Attach(item);
                this.Entry(item).State = EntityState.Modified;
                this.SaveChanges();
            }
        }
    }

    public class InitTables : DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<StreetNumber> StreetNumber { get; set; }
        public DbSet<Structure> Structure { get; set; }
        public DbSet<Zoo> Zoo { get; set; }

        public InitTables()
        {
        }
    }
}
