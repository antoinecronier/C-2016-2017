using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public ScheduleAdminVM(ScheduleAdmin scheduleAdmin)
        {
            this.scheduleAdmin = scheduleAdmin;

            InitUC();
            InitActions();
        }

        private void InitUC()
        {
            currentSchedule = new Schedule();
            this.scheduleAdmin.scheduleUC.Schedule = currentSchedule;
        }

        private void InitActions()
        {
/*          this.scheduleAdmin.btnDelete.Click += btnDeleteClick;
            this.scheduleAdmin.btnOk.Click += btnOkClick;
            this.scheduleAdmin.btnNew.Click += btnNewClick;
*/        }
        /*
        private void btnDeleteClick(object sender, RoutedEventArgs e)
        {
            scheduleManager.Delete(this.scheduleAdmin.scheduleUC.Schedule);
        }

        private void btnOkClick(object sender, RoutedEventArgs e)
        {
            scheduleManager.Update(this.scheduleAdmin.scheduleUC.Schedule);
        }

        private void btnNewClick(object sender, RoutedEventArgs e)
        {
            scheduleManager.Insert(this.scheduleAdmin.scheduleUC.Schedule);
        }
        */
    }
}
