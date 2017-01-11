using System;

namespace wpfzoo.entities
{
    public class Job
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
            }
        }
    }
}