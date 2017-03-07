using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PokemonLib.database;
using System.Threading.Tasks;

namespace PokeAPI.Controllers.BaseControllers
{
    public class BaseController<T> : ApiController where T : class
    {
        private MySQLManager<T> manager;

        public BaseController()
        {
            manager = new MySQLManager<T>();
        }

        [HttpGet]
        public async Task<IEnumerable<T>> Get()
        {
            return await manager.Get();
        }

        [HttpGet]
        public async Task<T> Get(int id)
        {
            return await manager.Get(id);
        }
    }
}