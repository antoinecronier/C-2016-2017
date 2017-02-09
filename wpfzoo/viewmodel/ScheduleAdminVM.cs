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

            InitUC();
            InitActions();
            InitScheduleNow();
            InitLists();
        }

        private void InitUC()
        {
            currentSchedule = new Schedule();
            this.scheduleAdmin.scheduleUC.Schedule = currentSchedule;
        }

        private void InitScheduleNow()
        {
            this.scheduleAdmin.scheduleUC.Schedule.Start = DateTime.Now;
            this.scheduleAdmin.scheduleUC.Schedule.End = DateTime.Now;
        }

        private async void InitLists()
        {
            this.scheduleAdmin.listScheduleUC.LoadItem((await scheduleManager.Get()).ToList());
        }

        private void AddInList()
        {
            this.scheduleAdmin.listScheduleUC.AddItem(this.scheduleAdmin.scheduleUC.Schedule);
        }

        private void SupInList()
        {
            this.scheduleAdmin.listScheduleUC.SupItem(this.scheduleAdmin.scheduleUC.Schedule);
        }

        private void InitActions()
        {
            this.scheduleAdmin.btnDelete.Click += btnDelete_Click;
            this.scheduleAdmin.btnOk.Click += btnOk_Click;
            this.scheduleAdmin.btnNew.Click += btnNew_Click;
            this.scheduleAdmin.listScheduleUC.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.scheduleAdmin.scheduleUC.Schedule.Id != 0)
            {
                await scheduleManager.Delete(this.scheduleAdmin.scheduleUC.Schedule);
                SupInList();
            }
        }

        private async void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int difDate = 0;

            if (this.scheduleAdmin.scheduleUC.Schedule != null)
            {
                difDate = DateTime.Compare(this.scheduleAdmin.scheduleUC.Schedule.Start, this.scheduleAdmin.scheduleUC.Schedule.End);
                if (difDate > 0)
                {
                    MessageBox.Show("Date of Start > Date of End", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (this.scheduleAdmin.scheduleUC.Schedule.Id > 0)
                {
                    await scheduleManager.Update(this.scheduleAdmin.scheduleUC.Schedule);
                }
                else
                {
                    await scheduleManager.Insert(this.scheduleAdmin.scheduleUC.Schedule);
                    AddInList();
                }
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            this.scheduleAdmin.scheduleUC.Schedule = new Schedule();
            InitScheduleNow();
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
