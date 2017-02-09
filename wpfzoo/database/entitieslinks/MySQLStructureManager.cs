using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities;

namespace wpfzoo.database.entitieslinks
{
    class MySQLStructureManager : MySQLManager<Structure>
    {
        public void GetEmployees(Structure structure)
        {
            this.DbSetT.Attach(structure);
            this.Entry(structure).Collection(x => x.AssignEmployees).Load();
        }

        public void GetAnimals(Structure structure)
        {
            this.DbSetT.Attach(structure);
            this.Entry(structure).Collection(x => x.AssignAnimals).Load();
        }

        public void GetSchedule(Structure structure)
        {
            this.DbSetT.Attach(structure);
            this.Entry(structure).Reference(x => x.Schedule).Load();
        }
    }
}
