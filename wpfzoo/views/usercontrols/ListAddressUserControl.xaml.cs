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
using wpfzoo.database;
using wpfzoo.entities;

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Logique d'interaction pour ListAddressUserControl.xaml
    /// </summary>
    public partial class ListAddressUserControl : UserControl
    {
        #region attributs
        private MySQLManager<Address> addressManager = new MySQLManager<Address>();
        #endregion

        #region properties
        public ListView ItemsList { get; set; }
        public ObservableCollection<Address> Obs { get; set; }
        #endregion

        #region constructor
        public ListAddressUserControl()
        {
            this.InitializeComponent();
            Obs = new ObservableCollection<Address>();
            this.itemList.ItemsSource = Obs;
            this.ItemsList = this.itemList;
            this.ItemsList.SelectionMode = SelectionMode.Single;
        }

        #endregion

        #region methods
        private void RemoveNutritionContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            Address itemToDelete = ItemsList.SelectedItem as Address;
            addressManager.Delete(itemToDelete);
            Obs.Remove(itemToDelete);  // remove the selected Item 
        }

        private void EditNutritionContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedIndex > -1)
            {
                var address = new Address();
                address = (Address)ItemsList.SelectedItem; // casting the list view 
                MessageBox.Show("You are in edit for Name:" + address.City, "Nutrition", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        /// <summary>
        /// Current list for User items.
        /// </summary>
        public void LoadItems(List<Address> items)
        {
            Obs.Clear();
            foreach (var item in items)
            {
                Obs.Add(item);
            }
        }

        public void AddItem(Address item)
        {
            Obs.Add(item);
        }

        public void RemoveItem(Address item)
        {
            Obs.Remove(item);
        }
        #endregion

        #region events
        #endregion
    }
}
