using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonLib.Entities
{
    public class PokemonResult
    {
        public string url { get; set; }
        public string name { get; set; }
    }

    public class PokemonBundle
    {
        public int count { get; set; }
        public object previous { get; set; }
        public List<PokemonResult> results { get; set; }
        public string next { get; set; }
    }
}
