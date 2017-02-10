using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            bool isDetached = this.Entry(address).State == EntityState.Detached;
            if (isDetached)
                this.DbSetT.Attach(address);
            this.Entry(address).Reference(x => x.StreetNumber).Load();
        }
    }
}
