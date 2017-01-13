using ClassLibrary2.Entities.Generator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;
using wpfzoo.json;

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
            : base(JsonManager.Instance.ReadFile<ConnectionString>(@"C:\Users\jéjé\Documents\DL2\C#\C-2016-2017\jsonconfig\", @"MysqlConfig.json").ToString())
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

                EntityGenerator<Animal> generatorAnimal = new EntityGenerator<Animal>();
                for (int i = 0; i < 10; i++)
                {
                    AnimalTable.Add(generatorAnimal.GenerateItem());
                }

                EntityGenerator<Employee> generatorEmployee = new EntityGenerator<Employee>();
                for (int i = 0; i < 10; i++)
                {
                    EmployeeTable.Add(generatorEmployee.GenerateItem());
                }

                EntityGenerator<Job> generatorJob = new EntityGenerator<Job>();
                for (int i = 0; i < 10; i++)
                {
                    JobTable.Add(generatorJob.GenerateItem());
                }

                EntityGenerator<Schedule> generatorSchedule = new EntityGenerator<Schedule>();
                for (int i = 0; i < 10; i++)
                {
                    ScheduleTable.Add(generatorSchedule.GenerateItem());
                }

                EntityGenerator<StreetNumber> generatorStreetNumber = new EntityGenerator<StreetNumber>();
                for (int i = 0; i < 10; i++)
                {
                    StreetNumberTable.Add(generatorStreetNumber.GenerateItem());
                }

                EntityGenerator<Structure> generatorStructure = new EntityGenerator<Structure>();
                for (int i = 0; i < 10; i++)
                {
                    StructureTable.Add(generatorStructure.GenerateItem());
                }

                EntityGenerator<Zoo> generatorZoo = new EntityGenerator<Zoo>();
                for (int i = 0; i < 1; i++)
                {
                    ZooTable.Add(generatorZoo.GenerateItem());
                }

                this.SaveChangesAsync();

/*
                MySQLManager<Address> addressManager = new MySQLManager<Address>();

                for (int i = 0; i < 10; i++)
                {
                    Task<Address> addressT = addressManager.Get(i);
                    Address address = (Address) addressT.AsyncState;

                    MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
                    Task<StreetNumber> addressT = streetNumberManager.Get(i);
                    Address address = (Address)addressT.AsyncState;
                    address.StreetNumber = 1;
                }

                MySQLManager<Employee> employeeManager = new MySQLManager<Employee>(); 
                MySQLManager<Job> jobManager = new MySQLManager<Job>();
                MySQLManager<Structure> structureManager = new MySQLManager<Structure>();
                MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
                */
                this.SaveChangesAsync();
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}