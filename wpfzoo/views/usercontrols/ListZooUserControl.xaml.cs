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
using wpfzoo.entities;

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Interaction logic for ListZooUserControl.xaml
    /// </summary>
    public partial class ListZooUserControl : UserControl
    {
        #region attributs
        #endregion

        #region properties
        public ListView ItemsList { get; set; }
        public ObservableCollection<Zoo> Obs { get; set; }
        #endregion

        #region constructor
        public ListZooUserControl()
        {
            this.InitializeComponent();
            Obs = new ObservableCollection<Zoo>();
            this.itemList.ItemsSource = Obs;
            this.ItemsList = this.itemList;
        }
        #endregion

        #region methods
        /// <summary>
        /// Current list for User items.
        /// </summary>
        public void LoadItem(List<Zoo> items)
        {
            Obs.Clear();
            foreach (var item in items)
            {
                Obs.Add(item);
            }
        }
        #endregion

        #region events
        #endregion

    }
}
