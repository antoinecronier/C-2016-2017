using System;
using wpfzoo.entities.bases;

namespace wpfzoo.entities
{
    public class Schedule : BaseEntity
    {
        private DateTime start;
        private DateTime end;

        public DateTime Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
                OnPropertyChanged("Start");
            }
        }

        public DateTime End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
                OnPropertyChanged("End");
            }
        }
    }
}