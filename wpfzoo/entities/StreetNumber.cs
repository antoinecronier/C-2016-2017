using System;
using wpfzoo.entities.bases;
using wpfzoo.entities.enums;

namespace wpfzoo.entities
{
    public class StreetNumber : BaseEntity
    {
        private Int32 number;
        private StreetAvaibleItems suf;

        public StreetAvaibleItems Suf
        {
            get { return suf; }
            set
            {
                suf = value;
                OnPropertyChanged("Suf");
            }
        }

        public Int32 Number
        {
            get { return number; }
            set
            { number = value;
              OnPropertyChanged("Number");
            }
        }

        public StreetNumber()
        {
        }
    }
}