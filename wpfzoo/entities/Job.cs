using System;
using wpfzoo.entities.bases;

namespace wpfzoo.entities
{
    public class Job : BaseDBEntity
    {
        private String name;
        private Decimal salary;
        private Schedule schedule;



        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public decimal Salary
        {
            get
            {
                return salary;
            }

            set
            {
                salary = value;
                OnPropertyChanged("Salary");
            }
        }

        public Schedule Schedule
        {
            get
            {
                return schedule;
            }

            set
            {
                schedule = value;
                OnPropertyChanged("Schedule");
            }
        }
    }
}