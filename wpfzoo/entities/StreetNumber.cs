using System;
using wpfzoo.entities.enums;

namespace wpfzoo.entities
{
    public class StreetNumber
    {
        private Int32 number;
        private StreetAvaibleItems suf;

        public StreetAvaibleItems Suf
        {
            get { return suf; }
            set { suf = value; }
        }

        public Int32 Number
        {
            get { return number; }
            set { number = value; }
        }

    }
}