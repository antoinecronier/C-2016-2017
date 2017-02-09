using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;

namespace wpfzoo.database.entitieslinks
{
    class MySQLJobManager : MySQLManager<Job>
    {
        public void GetSchedule(Job job)
        {
            this.DbSetT.Attach(job);
            this.Entry(job).Reference(x => x.Schedule).Load();
        }
    }
}
