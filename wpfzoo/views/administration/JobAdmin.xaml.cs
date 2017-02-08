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
using System.Collections.ObjectModel;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Interaction logic for JobAdmin.xaml
    /// </summary>
    public partial class JobAdmin : Page
    {
        ObservableCollection<Job> jobList = new ObservableCollection<Job>();
        ObservableCollection<Schedule> scheduleList = new ObservableCollection<Schedule>();
        public JobAdmin()
        {
            InitializeComponent();
            new MySQLFullDB();
            this.UCJobList.itemList.SelectionChanged += ItemList_SelectionChanged;
            InitLists();
        }

        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Job job = (e.AddedItems[0] as Job);
            }
        }
        private async void InitLists()
        {
            MySQLManager<Job> jobManager = new MySQLManager<Job>();
            this.UCJobList.LoadItem((await jobManager.Get()).ToList());

            //MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();
            //this.UCScheduleList.LoadItem((await scheduleManager.Get()).ToList());
        }

        

        //
        //private Job job;
        //private String scheduleToString;

        //public event PropertyChangedEventHandler PropertyChanged;

        //public Job Job
        //{
        //    get { return job; }
        //    set { job = value; }
        //}

        //public String ScheduleToString
        //{
        //    get
        //    {
        //        return job.Schedule.Start.ToString() + job.Schedule.End.ToString();
        //    }

        //    set
        //    {
        //        scheduleToString = value;
        //        //OnPropertyChanged("ScheduleToString");
        //    }
        //}

        //public JobAdmin()
        //{
        //    InitializeComponent();
        //    MySQLFullDB fullDb = new MySQLFullDB();
        //    //Schedule schedule = new Schedule();
        //    //schedule.Start = DateTime.Now;
        //    //schedule.End = DateTime.Now;
        //    //this.scheduleUC.Schedule = schedule;
        //}
    }
}
