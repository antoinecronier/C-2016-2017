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
using wpfzoo.entities;
using wpfzoo.entities.bases;
using System.ComponentModel;
using wpfzoo.database;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Interaction logic for JobAdministration.xaml
    /// </summary>
    public partial class JobAdministration : Page, INotifyPropertyChanged
    {
        private Job job;
        private String scheduleToString;

        public event PropertyChangedEventHandler PropertyChanged;

        public Job Job
        {
            get { return job; }
            set { job = value; }
        }

        public String ScheduleToString
        {
            get
            {
                return job.Schedule.Start.ToString() + job.Schedule.End.ToString();
            }

            set
            {
                scheduleToString = value;
                //OnPropertyChanged("ScheduleToString");
            }
        }

        public JobAdministration()
        {
            InitializeComponent();
            MySQLFullDB fullDb = new MySQLFullDB();
            //Schedule schedule = new Schedule();
            //schedule.Start = DateTime.Now;
            //schedule.End = DateTime.Now;
            //this.scheduleUC.Schedule = schedule;
        }
    }
}
