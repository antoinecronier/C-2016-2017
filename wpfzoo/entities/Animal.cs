using System;
using wpfzoo.entities.bases;
using wpfzoo.entities.enums;

namespace wpfzoo.entities
{
    public class Animal : BaseEntity
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

        public Decimal Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
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
            }
        }
    }
}