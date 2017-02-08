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

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Interaction logic for JobUserControl.xaml
    /// </summary>
    public partial class JobUserControl : UserControlBase
    {
        private Job job;

        public Job Job
        {
            get { return job; }
            set {
                job = value;
                base.OnPropertyChanged("Job");
            }
        }

        public JobUserControl()
        {
            InitializeComponent();
            base.DataContext = this;
            if (Job != null)
            {
                this.ucSchedule.Schedule = Job.Schedule;
            }
        }
    }
}
