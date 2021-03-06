﻿using ClassLibrary2.Entities.Generator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.viewmodel;
using wpfzoo.views.usercontrols;

namespace wpfzoo.views.administration
{

    /// <summary>
    /// Logique d'interaction pour AnimalAdmin.xaml
    /// </summary>
    public partial class AnimalAdmin : Page
    {
        public AnimalAdmin()
        {
            InitializeComponent();
            DataContext = new AnimalAdminVM(this);
        }
    }
}