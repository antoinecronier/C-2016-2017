using System;

namespace wpfzoo.entities
{
    public class Schedule
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
            }
        }
    }
}