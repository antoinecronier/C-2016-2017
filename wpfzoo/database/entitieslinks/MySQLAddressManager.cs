using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;

namespace wpfzoo.database.entitieslinks
{
    public class MySQLAddressManager : MySQLManager<Address>
    {
        public void GetStreetNumber(Address address)
        {
            this.DbSetT.Attach(address);
            this.Entry(address).Reference(x => x.StreetNumber).Load();
        }
    }
}
