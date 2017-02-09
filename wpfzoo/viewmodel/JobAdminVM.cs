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
using wpfzoo.entities;
using wpfzoo.views.administration;

namespace wpfzoo.viewmodel
{
    class JobAdminVM
    {
        private Job currentJob;
        private JobAdmin jobAdmin;
        private MySQLManager<Job> jobManager = new MySQLManager<Job>();

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
                this.jobAdmin.UCJob.Job = item;
            }
        }

        private void InitUC()
        {
            currentJob = new Job();
            this.jobAdmin.UCJob.Job = currentJob;
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
            var regexName = new Regex(@"^[A-Z][-a-zA-Z]+$");
            var regexSalary = new Regex(@"[0-9]+(\.[0-9][0-9]?)?");

            if (regexName.Match(job.Name).Success && regexSalary.Match(job.Salary.ToString()).Success) {
                return true;
            } else
            {
                System.Windows.MessageBox.Show("Please check fields");
                return false;
            }
        }

        private async void btnAddJob_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkValidity(this.jobAdmin.UCJob.Job))
            {
                await jobManager.Insert(this.jobAdmin.UCJob.Job);
                InitLUC();
            } else
            {

            }
        }
    }
}
