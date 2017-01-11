using System;
using System.Collections.Generic;
using wpfzoo.entities.enums;

namespace wpfzoo.entities
{
    public class Employee
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
            }
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
            }
        }
    }
}