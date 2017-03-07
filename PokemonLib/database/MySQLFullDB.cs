using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonLib.Entities;

namespace PokemonLib.database
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MySQLFullDB : DbContext
    {
        public DbSet<Sprites> SpritesTable { get; set; }
        public DbSet<Stat> StatTable { get; set; }
        public DbSet<Stat2> Stat2Table { get; set; }
        public DbSet<Pokemon> PokemonTable { get; set; }

        public MySQLFullDB()
            : base("Server=127.0.0.1;Port=3306;Database=pokemonZoo;Uid=root;Pwd=''")
        {
            InitLocalMySQL();
        }

        public void InitLocalMySQL()
        {
            if (this.Database.CreateIfNotExists())
            {
                
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
