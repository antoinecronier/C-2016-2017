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
    /// Logique d'interaction pour ScheduleUserControl.xaml
    /// </summary>
    public partial class ScheduleUserControl : UserControlBase
    {
        private Schedule schedule;

        public Schedule Schedule
        {
            get { return schedule; }
            set {
                schedule = value;
                base.OnPropertyChanged("Schedule");
            }
        }

        public ScheduleUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
