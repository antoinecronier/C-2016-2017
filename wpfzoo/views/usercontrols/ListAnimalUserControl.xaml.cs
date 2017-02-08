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
    /// Logique d'interaction pour ListAnimalUserControl.xaml
    /// </summary>
    public partial class ListAnimalUserControl : UserControl
    {
        #region attributs
        #endregion

        #region properties
        public ListView ItemsList { get; set; }
        public ObservableCollection<Animal> Obs { get; set; }
        public Animal Animal { get; internal set; }
        #endregion

        #region constructor
        public ListAnimalUserControl()
        {
            this.InitializeComponent();
            Obs = new ObservableCollection<Animal>();
            this.itemList.ItemsSource = Obs;
            this.ItemsList = this.itemList;
            this.ItemsList.SelectionMode = SelectionMode.Single;
        }

        #endregion

        #region methods
        private void RemoveAnimalContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            Obs.Remove(ItemsList.SelectedItem as Animal);
        }

        private void EditAnimalContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedIndex > -1)
            {
                var address = new Animal();
                address = (Animal)ItemsList.SelectedItem;
                MessageBox.Show("You are in edit for Name:" + Name, "Animal", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        /// <summary>
        /// Current list for User items.
        /// </summary>
        /// 
        public void LoadItem(List<Animal> items)
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
