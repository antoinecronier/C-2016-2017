using System;
using System.Collections.Generic;
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
        public ScheduleAdmin()
        {
            InitializeComponent();

            Schedule schedule = new Schedule();
            schedule.Start = DateTime.Now;
            schedule.End = DateTime.Now;
            this.scheduleUC.Schedule = schedule;
        }

        private void btnDeleteClick(object sender, RoutedEventArgs e)
        {
            MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();
            scheduleManager.Delete();
        }

        private void btnOkClick(object sender, RoutedEventArgs e)
        {
            MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();
            if (scheduleManager.Get(sender))
            {
                scheduleManager.Update(sender);
            }
            else
            {
                scheduleManager.Insert(sender);
            }
        }

        private void btnNewClick(object sender, RoutedEventArgs e)
        {
            MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();
            scheduleManager.Insert(sender);
        }

        private void scrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
