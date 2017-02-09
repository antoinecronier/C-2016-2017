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
    /// Interaction logic for ListScheduleUserControl.xaml
    /// </summary>
    public partial class ListScheduleUserControl : UserControl
    {
        #region attributs
        #endregion

        #region properties
        public ListView ItemsList { get; set; }
        public ObservableCollection<Schedule> Obs { get; set; }
        MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();

        #endregion

        #region constructor
        public ListScheduleUserControl()
        {
            this.InitializeComponent();
            Obs = new ObservableCollection<Schedule>();
            this.itemList.ItemsSource = Obs;
            this.ItemsList = this.itemList;
            this.ItemsList.SelectionMode = SelectionMode.Single;
        }
        #endregion

        #region methods
        private async void RemoveNutritionContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            var schedule = new Schedule();
            schedule = ItemsList.SelectedItem as Schedule;
            if (schedule.Id > 0 && schedule != null)
            {
                await scheduleManager.Delete(schedule);
                Obs.Remove(schedule);  // remove the selected Item 
                MessageBox.Show("You are in remove schedule for:\nStart : " + schedule.Start + "\nEnd : " + schedule.End, "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void DuplicateNutritionContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedIndex > -1)
            {
                var schedule = new Schedule();
                var scheduleTmp = new Schedule();

                schedule = ItemsList.SelectedItem as Schedule; // casting the list view 
                if (schedule.Id > 0 && schedule != null)
                {
                    scheduleTmp.Start = schedule.Start;
                    scheduleTmp.End = schedule.End;
                    scheduleTmp.Id = 0;
                    await scheduleManager.Insert(scheduleTmp);
                    Obs.Add(scheduleTmp);
                    MessageBox.Show("You are in duplicate schedule for:\nStart : " + scheduleTmp.Start + "\nEnd : " + scheduleTmp.End, "Duplicate", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
