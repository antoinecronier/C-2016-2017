using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonLib.Entities.BaseEntities;

namespace PokemonLib.Entities
{
    public class Pokemon : EntityBase
    {
        public List<Stat> stats { get; set; }
        public string name { get; set; }
        public int weight { get; set; }
        public Sprites sprites { get; set; }
        public int height { get; set; }
        //public int id { get; set; }
        public int order { get; set; }
    }

    public class Stat2 : EntityBase
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class Stat : EntityBase
    {
        public Stat2 stat { get; set; }
        public int effort { get; set; }
        public int base_stat { get; set; }
    }

    public class Sprites : EntityBase
    {
        public object back_female { get; set; }
        public object back_shiny_female { get; set; }
        public string back_default { get; set; }
        public object front_female { get; set; }
        public object front_shiny_female { get; set; }
        public string back_shiny { get; set; }
        public string front_default { get; set; }
        public string front_shiny { get; set; }
    }
}
