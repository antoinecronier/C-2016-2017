using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PokemonLib.Entities.BaseEntities
{
    public class EntityBase
    {
        private int id;

        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
