using System;
using System.Collections.Generic;
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
            await jobManager.Update(this.jobAdmin.UCJob.Job);
            InitLUC();
        }

        private async void btnAddJob_Click(object sender, RoutedEventArgs e)
        {
            await jobManager.Insert(this.jobAdmin.UCJob.Job);
            InitLUC();
        }
    }
}
