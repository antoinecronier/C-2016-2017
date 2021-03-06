﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using wpfzoo.json;
using wpfzoo.viewmodel;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour StructureAdmin.xaml
    /// </summary>
    public partial class StructureAdmin : Page
    {
         public StructureAdmin()
        {
            InitializeComponent();
            this.DataContext = new StructureAdminVM(this);
            InitLists();
        }

        private async void InitLists()
        {
            MySQLManager<Structure> structureManager = new MySQLManager<Structure>();
            this.UCstructureList.LoadItem((await structureManager.Get()).ToList());
        }

        public StructureAdmin(ZooAdminVM zooViewModel)
        {
            InitializeComponent();
            this.DataContext = zooViewModel;
            zooViewModel.LoadStructurePage(this);
            InitLists();
        }
    }
}
