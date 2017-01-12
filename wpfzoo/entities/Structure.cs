using System;
using System.Collections.Generic;
using wpfzoo.entities.bases;

namespace wpfzoo.entities
{
    public class Structure : BaseDBEntity
    {
        private String name;
        private float surface;
        private List<Employee> assignEmployees;
        private List<Animal> assignAnimals;
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

        public float Surface
        {
            get
            {
                return surface;
            }

            set
            {
                surface = value;
                OnPropertyChanged("Surface");
            }
        }

        public List<Employee> AssignEmployees
        {
            get
            {
                return assignEmployees;
            }

            set
            {
                assignEmployees = value;
            }
        }

        internal List<Animal> AssignAnimals
        {
            get
            {
                return assignAnimals;
            }

            set
            {
                assignAnimals = value;
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

        public Structure()
        {
            this.assignAnimals = new List<Animal>();
            this.assignEmployees = new List<Employee>();

        }
    }
}