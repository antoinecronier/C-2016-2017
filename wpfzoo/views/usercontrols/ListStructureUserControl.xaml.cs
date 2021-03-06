﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Interaction logic for ListStructureUserControl.xaml
    /// </summary>
    public partial class ListStructureUserControl : UserControl
    {
        #region attributs
        #endregion
        
        #region properties
        public ListView ItemsList { get; set; }
        public ObservableCollection<Structure> Obs { get; set; }
        public MySQLManager<Structure> mySQLManager;
        #endregion


        #region constructor
        public ListStructureUserControl()
        {
            InitializeComponent();
            mySQLManager = new MySQLManager<Structure>();
            Obs = new ObservableCollection<Structure>();
            this.itemList.ItemsSource = Obs;
            this.ItemsList = this.itemList;
        }

        #endregion

        #region methods
        /// <summary>
        /// Current list for User items.
        /// </summary>
        public void LoadItem(List<Structure> items)
        {
            Obs.Clear();
            foreach (Structure structure in items)
            {
                    Obs.Add(structure);
            }
        }

        public void AddItem(Structure structure)
        {
            Obs.Add(structure);
        }
        #endregion

        private void CMDuplicate_Click(object sender, RoutedEventArgs e)
        {
            Structure duplicatedStructure = new Structure();
            Structure structureToDuplicate = new Structure();
            structureToDuplicate = ItemsList.SelectedItem as Structure;
            duplicatedStructure.Id = 0;
            duplicatedStructure.Name = structureToDuplicate.Name;
            duplicatedStructure.Schedule = structureToDuplicate.Schedule;
            duplicatedStructure.Surface = structureToDuplicate.Surface;
            Obs.Add(duplicatedStructure);
            mySQLManager.Insert(duplicatedStructure);
        }

        private void CMRemove_Click(object sender, RoutedEventArgs e)
        {
            mySQLManager.Delete(ItemsList.SelectedItem as Structure);
            Obs.Remove(ItemsList.SelectedItem as Structure);
        }
    }

        #region events
        #endregion
    }

