using System;
using wpfzoo.entities.bases;
using wpfzoo.entities.enums;

namespace wpfzoo.entities
{
    public class Animal : BaseDBEntity
    {
        private String name;
        private DateTime birth;
        private Gender gender;
        private Decimal weight;
        private Decimal height;
        private Boolean isDead;

        public String Name
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

        public Decimal Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
                OnPropertyChanged("Weight");
            }
        }

        public Decimal Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public Boolean IsDead
        {
            get
            {
                return isDead;
            }

            set
            {
                isDead = value;
                OnPropertyChanged("IsDead");
            }
        }
        
        public Animal()
        {
        }
    }
}
