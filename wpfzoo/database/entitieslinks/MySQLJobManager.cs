using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            bool isDetached = this.Entry(job).State == EntityState.Detached;
            if (isDetached)
                this.DbSetT.Attach(job);
            this.Entry(job).Reference(x => x.Schedule).Load();
        }
    }
}
