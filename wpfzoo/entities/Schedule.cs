using System;
using wpfzoo.entities.bases;

namespace wpfzoo.entities
{
    public class Schedule : BaseDBEntity
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