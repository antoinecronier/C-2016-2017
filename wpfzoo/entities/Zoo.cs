using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.entities.bases;

namespace wpfzoo.entities
{
    public class Zoo : BaseEntity
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
                OnPropertyChanged("Name");
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
                OnPropertyChanged("Address");
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
                OnPropertyChanged("Staff");
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
                OnPropertyChanged("structures");
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
                OnPropertyChanged("birth");
            }
        }
        public Zoo()
        {
        }
    }
}
