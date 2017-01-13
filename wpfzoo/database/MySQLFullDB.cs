using ClassLibrary2.Entities.Generator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;

namespace wpfzoo.database
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MySQLFullDB : DbContext
    {
        public DbSet<Address> AddressTable { get; set; }
        public DbSet<Animal> AnimalTable { get; set; }
        public DbSet<Employee> EmployeeTable { get; set; }
        public DbSet<Job> JobTable { get; set; }
        public DbSet<Schedule> ScheduleTable { get; set; }
        public DbSet<StreetNumber> StreetNumberTable { get; set; }
        public DbSet<Structure> StructureTable { get; set; }
        public DbSet<Zoo> ZooTable { get; set; }

        public MySQLFullDB()
            : base("Server=127.0.0.1;Port=3306;Database=zoo;Uid=root;Pwd=''")
        {
                    InitLocalMySQL();
        }

        public void InitLocalMySQL()
        {
            if (this.Database.CreateIfNotExists())
            {
                EntityGenerator<Address> generatorAddress = new EntityGenerator<Address>();
                for (int i = 0; i < 10; i++)
                {
                    AddressTable.Add(generatorAddress.GenerateItem());
                }

                EntityGenerator<StreetNumber> generatorStreetNumber = new EntityGenerator<StreetNumber>();
                for (int i = 0; i < 10; i++)
                {
                    StreetNumberTable.Add(generatorStreetNumber.GenerateItem());
                }

                this.SaveChangesAsync();

                AddressTable.Find(1).StreetNumber = StreetNumberTable.Find(1);

                this.SaveChangesAsync();
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}