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
using wpfzoo.viewmodel;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour AddressAdmin.xaml
    /// </summary>
    public partial class AddressAdmin : Page
    {
        private AddressAdmin addressAdmin;

        public AddressAdmin()
        {
            InitializeComponent();
            this.DataContext = new AddressAdminVM(this);
        }
    }
}
