﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfzoo.entities.bases
{
    public class BaseDBEntity : BaseEntity
    {
        private Int32 id;

        public Int32 Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

    }
}
