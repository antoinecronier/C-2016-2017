using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;

namespace wpfzoo.database.entitieslinks
{
    public class MySQLEmployeeManager : MySQLManager<Employee>
    {
        public void GetJobs(Employee employee)
        {
            this.Entry(employee).Collection(x => x.Jobs).Load();
        }

        public void GetAddress(Employee employee)
        {
            this.Entry(employee).Reference(x => x.Address).Load();
        }
    }
}
