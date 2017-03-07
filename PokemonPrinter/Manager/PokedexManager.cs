using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonPrinter.WebService;
using PokemonLib.Entities;
using System.IO;
using Newtonsoft.Json;

namespace PokemonPrinter.Manager
{
    public class PokedexManager
    {
        WebServiceManager webServiceManager = new WebServiceManager();

        public async Task<List<Pokemon>> GetWebBundle(int number, Boolean backup = false)
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            int stepBundle = 0;

            PokemonBundle bundle = await webServiceManager.GetData<PokemonBundle>("pokemon",new PokemonBundle());
            foreach (var item in bundle.results)
            {
                pokemons.Add(await webServiceManager.GetData<Pokemon>(item.url, new Pokemon()));
            }
            number -= 20;

            if (backup)
            {
                using (StreamWriter file = File.CreateText(@"../../../pokedexJson/pokedex" + stepBundle + ".json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, pokemons);
                }
                stepBundle += 20;
            }

            while (number > 0)
            {
                List<Pokemon> pokemonsSub = new List<Pokemon>();
                bundle = await webServiceManager.GetData<PokemonBundle>(bundle.next.Replace("https://pokeapi.co/api/v2/",""), new PokemonBundle());
                foreach (var item in bundle.results)
                {
                    pokemonsSub.Add(await webServiceManager.GetData<Pokemon>(item.url, new Pokemon()));
                }

                number -= 20;

                if (backup)
                {
                    using (StreamWriter file = File.CreateText(@"../../../pokedexJson/pokedex" + stepBundle + ".json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, pokemons);
                    }
                    stepBundle += 20;
                }

                pokemons.AddRange(pokemonsSub);
            }

            

            return pokemons;
        }
    }
}
