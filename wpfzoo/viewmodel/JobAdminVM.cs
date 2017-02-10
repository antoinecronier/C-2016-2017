using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using wpfzoo.database;
using wpfzoo.database.entitieslinks;
using wpfzoo.entities;
using wpfzoo.views.administration;

namespace wpfzoo.viewmodel
{
    class JobAdminVM
    {
        private Job currentJob;
        private JobAdmin jobAdmin;
        private MySQLJobManager jobManager = new MySQLJobManager();
//        private MySQLManager<Job> jobManager = new MySQLManager<Job>();
        private MySQLManager<Schedule> scheduleManager = new MySQLManager<Schedule>();

        public JobAdminVM(JobAdmin jobAdmin)
        {
            this.jobAdmin = jobAdmin;
            InitUC();
            InitLUC();
            InitActions();
        }


        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Job item = (e.AddedItems[0] as Job);
                if (item.Schedule == null)
                {
                    item.Schedule = new Schedule();
                }
                jobManager.GetSchedule(item);
                this.jobAdmin.UCJob.Job = item;
                this.jobAdmin.UCJob.ucSchedule.Schedule = item.Schedule;
            }
        }

        private void InitUC()
        {
            currentJob = new Job();
            this.jobAdmin.UCJob.Job = currentJob;
            this.jobAdmin.UCJob.ucSchedule.Schedule = new Schedule();
        }

        private async void InitLUC()
        {
            this.jobAdmin.UCJobList.LoadItem((await jobManager.Get()).ToList());
        }

        private void InitActions()
        {
            this.jobAdmin.btnAddJob.Click += btnAddJob_Click;
            this.jobAdmin.btnUpdateJob.Click += btnUpdateJob_Click;
            this.jobAdmin.btnDelJob.Click += btnDelJob_Click;
            this.jobAdmin.UCJobList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        private async void btnDelJob_Click(object sender, RoutedEventArgs e)
        {
            await jobManager.Delete(this.jobAdmin.UCJob.Job);
            InitLUC();
            InitUC();
        }

        private async void btnUpdateJob_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkValidity(this.jobAdmin.UCJob.Job))
            {
                await jobManager.Update(this.jobAdmin.UCJob.Job);
                InitLUC();
            }
        }

        private Boolean checkValidity(Job job)
        {
            var regexName = new Regex(@"[a-zA-Z ]{3,30}");
            var regexSalary = new Regex(@"\d+(,\d{1,2})?");

            if (regexName.IsMatch(job.Name) && regexSalary.IsMatch(job.Salary.ToString())) {
                return true;
            } else
            {
                Boolean matchName = regexName.IsMatch(job.Name);
                Boolean matchSalary = regexSalary.IsMatch(job.Salary.ToString());
                System.Windows.MessageBox.Show("Please check fields");
                return false;
            }
        }

        private async void btnAddJob_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkValidity(this.jobAdmin.UCJob.Job))
            {
                this.jobAdmin.UCJob.Job.Schedule = this.jobAdmin.UCJob.ucSchedule.Schedule;
                await jobManager.Insert(this.jobAdmin.UCJob.Job);
                InitLUC();
            }
        }
    }
}
