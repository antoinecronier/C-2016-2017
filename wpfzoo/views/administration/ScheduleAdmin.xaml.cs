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

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour scheduleAdmin.xaml
    /// </summary>
    public partial class ScheduleAdmin : Page
    {
        ObservableCollection<Schedule> scheduleList = new ObservableCollection<Schedule>();
        public ScheduleAdmin()
        {
            InitializeComponent();
            this.listScheduleUC.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            InitLists();
        }

        private async void InitLists()
        {
            MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();
            this.listScheduleUC.LoadItem((await scheduleManager.Get()).ToList());
        }

        private void btnDeleteClick(object sender, RoutedEventArgs e)
        {
            MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();
            this.listScheduleUC.Obs.Remove(scheduleUC.Schedule);
            scheduleManager.Delete(this.scheduleUC.Schedule);
        }

        private void btnOkClick(object sender, RoutedEventArgs e)
        {
            MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();
            scheduleManager.Update(this.scheduleUC.Schedule);
        }

        private void btnNewClick(object sender, RoutedEventArgs e)
        {
            MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();
            scheduleManager.Insert(this.scheduleUC.Schedule);
        }

        private void listScheduleUC_Loaded(object sender, SelectedCellsChangedEventArgs e)
        {
//            this.listScheduleUC.LoadItem();
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Address item = (e.AddedItems[0] as Address);

            }
        }
    }
}
