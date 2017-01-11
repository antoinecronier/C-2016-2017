using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfzoo.entities
{
    public class Zoo
    {
        private String name;
        private Address address;
        private List<Employee> staff;
        private List<Structure> structures;
        private DateTime birth;

        public String Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public Address Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        public List<Employee> Staff
        {
            get
            {
                return this.staff;
            }
            set
            {
                this.staff = value;
            }
        }
        public List<Structure> Structures
        {
            get
            {
                return this.structures;
            }
            set
            {
                this.structures = value;
            }
        }
        public DateTime Birth
        {
            get
            {
                return this.birth;
            }
            set
            {
                this.birth = value;
            }
        }
        public Zoo()
        {
        }
    }
}
