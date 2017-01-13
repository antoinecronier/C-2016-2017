using System;
using System.Collections.Generic;
using System.Windows.Forms;
using wpfzoo.entities.bases;
using wpfzoo.entities.enums;

namespace wpfzoo.entities
{
    public class Employee : BaseDBEntity
    {
        private String lastname;
        private String firstname;
        private DateTime birth;
        private DateTime hiring;
        private List<Job> jobs;
        private Address address;
        private Gender gender;
        private Employee manager;
        private Dictionary<Schedule,Structure> planning;

        public String Lastname
        {
            get
            {
                return lastname;
            }

            set
            {
                lastname = value;
                OnPropertyChanged("Lastname");
            }
        }

        public String Firstname
        {
            get
            {
                return firstname;
            }

            set
            {
                firstname = value;
                OnPropertyChanged("Firstname");
            }
        }

        public DateTime Birth
        {
            get
            {
                return birth;
            }

            set
            {
                birth = value;
                OnPropertyChanged("Birth");
            }
        }

        public DateTime Hiring
        {
            get
            {
                return hiring;
            }

            set
            {
                hiring = value;
                OnPropertyChanged("Hiring");
            }
        }

        public static explicit operator Employee(ListViewItemSelectionChangedEventArgs v)
        {
            throw new NotImplementedException();
        }

        public List<Job> Jobs
        {
            get
            {
                return jobs;
            }

            set
            {
                jobs = value;
                OnPropertyChanged("Jobs");
            }
        }

        public Address Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public Gender Gender
        {
            get
            {
                return gender;
            }

            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public Employee Manager
        {
            get
            {
                return manager;
            }

            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        public Dictionary<Schedule, Structure> Planning
        {
            get
            {
                return planning;
            }

            set
            {
                planning = value;
                OnPropertyChanged("Planning");
            }
        }

        public Employee()
        {
            this.planning = new Dictionary<Schedule, Structure>();
            this.jobs = new List<Job>();
        }
    }
}