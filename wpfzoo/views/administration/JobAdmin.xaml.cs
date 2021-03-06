﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfzoo.entities;
using wpfzoo.entities.bases;
using System.ComponentModel;
using wpfzoo.database;
using System.Collections.ObjectModel;
using wpfzoo.viewmodel;

namespace wpfzoo.views.administration
{
    public partial class JobAdmin : Page
    {
        public JobAdmin()
        {
            InitializeComponent();
            this.DataContext = new JobAdminVM(this);
        }

        public JobAdmin(EmployeeAdminVM empAdminVM)
        {
            InitializeComponent();
            this.DataContext = empAdminVM;
            empAdminVM.LoadJobAdmin(this);
        }
    }
}
