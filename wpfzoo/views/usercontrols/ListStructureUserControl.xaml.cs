using System;
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
    /// Interaction logic for ListStructureUserControl.xaml
    /// </summary>
    public partial class ListStructureUserControl : UserControl
    {
        #region attributs
        #endregion
        
        #region properties
        public ListView ItemsList { get; set; }
        public ObservableCollection<Structure> Obs { get; set; }
        #endregion


        #region constructor
        public ListStructureUserControl()
        {
            InitializeComponent();
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

        private void itemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

        #region events
        #endregion
    }

