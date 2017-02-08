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
    /// Interaction logic for ListScheduleUserControl.xaml
    /// </summary>
    public partial class ListScheduleUserControl : UserControl
    {
        #region attributs
        #endregion

        #region properties
        public ListView ItemsList { get; set; }
        public ObservableCollection<Schedule> Obs { get; set; }
        #endregion

        #region constructor
        public ListScheduleUserControl()
        {
            this.InitializeComponent();
            Obs = new ObservableCollection<Schedule>();
            this.itemList.ItemsSource = Obs;
            this.ItemsList = this.itemList;
        }
        #endregion

        #region methods
        private void RemoveNutritionContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            Obs.Remove(ItemsList.SelectedItem as Schedule);  // remove the selected Item 
        }

        private void EditNutritionContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedIndex > -1)
            {
                var schedule = new Schedule();
                schedule = (Schedule)ItemsList.SelectedItem; // casting the list view 
                MessageBox.Show("You are in edit for Name:" + schedule.Start, "Nutrition", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
        /// <summary>
        /// Current list for User items.
        /// </summary>
        public void LoadItem(List<Schedule> items)
        {
            Obs.Clear();
            foreach (var item in items)
            {
                Obs.Add(item);
            }
        }

        public void AddItem(Schedule item)
        {
            Obs.Add(item);
        }

        public void SupItem(Schedule item)
        {
            Obs.Remove(item);
        }

        #endregion

        #region events
        #endregion
    }
}
