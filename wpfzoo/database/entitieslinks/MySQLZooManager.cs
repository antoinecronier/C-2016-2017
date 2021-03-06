﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;

namespace wpfzoo.database.entitieslinks
{
    public class MySQLZooManager : MySQLManager<Zoo>
    {
        public void GetAddress(Zoo zoo)
        {
            bool isDetached = this.Entry(zoo).State == EntityState.Detached;
            if (isDetached)
                this.DbSetT.Attach(zoo);
            this.Entry(zoo).Reference(x => x.Address).Load();
        }

        public void GetEmployees(Zoo zoo)
        {
            bool isDetached = this.Entry(zoo).State == EntityState.Detached;
            if (isDetached)
                this.DbSetT.Attach(zoo);
            this.Entry(zoo).Collection(x => x.Staff).Load();
        }

        public void GetStructures(Zoo zoo)
        {
            bool isDetached = this.Entry(zoo).State == EntityState.Detached;
            if (isDetached)
                this.DbSetT.Attach(zoo);
            this.Entry(zoo).Collection(x => x.Structures).Load();
        }
    }
}