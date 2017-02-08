using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;

namespace wpfzoo.viewmodel
{
    public class ScheduleAdminVM
    {
        private Schedule currentSchedule;
        private ScheduleAdmin scheduleAdmin;
        MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();
        ObservableCollection<Schedule> scheduleList = new ObservableCollection<Schedule>();


        public ScheduleAdminVM(ScheduleAdmin scheduleAdmin)
        {
            this.scheduleAdmin = scheduleAdmin;
            this.scheduleAdmin.listScheduleUC.ItemsList.SelectionChanged += ItemsList_SelectionChanged;

            InitUC();
            InitLists();
            InitActions();
        }

        private void InitUC()
        {
            currentSchedule = new Schedule();
            this.scheduleAdmin.scheduleUC.Schedule = currentSchedule;
        }

        private async void InitLists()
        {
            this.scheduleAdmin.listScheduleUC.LoadItem((await scheduleManager.Get()).ToList());
        }

        private void InitActions()
        {
            this.scheduleAdmin.btnDelete.Click += btnDelete_Click;
            this.scheduleAdmin.btnOk.Click += btnOk_Click;
            this.scheduleAdmin.btnNew.Click += btnNew_Click;
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.scheduleAdmin.scheduleUC.Schedule.Id != 0)
            {
                await scheduleManager.Delete(this.scheduleAdmin.scheduleUC.Schedule);
                InitLists();
            }
        }

        private async void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.scheduleAdmin.scheduleUC.Schedule.Id != 0)
            {
                await scheduleManager.Update(this.scheduleAdmin.scheduleUC.Schedule);
            }
            else
            {
                await scheduleManager.Insert(this.scheduleAdmin.scheduleUC.Schedule);
            }
        }

        private async void btnNew_Click(object sender, RoutedEventArgs e)
        {
            await scheduleManager.Insert(this.scheduleAdmin.scheduleUC.Schedule);
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Schedule item = (e.AddedItems[0] as Schedule);
                this.scheduleAdmin.scheduleUC.Schedule = item;
            }
        }
    }
}
