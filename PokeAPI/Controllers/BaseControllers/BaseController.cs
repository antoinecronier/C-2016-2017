using PokemonLib.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

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
        public virtual async Task<IEnumerable<T>> Get()
        {
            return await manager.Get();
        }

        [HttpGet]
        public virtual async Task<T> Get(int id)
        {
            return await manager.Get(id);
        }
    }
}