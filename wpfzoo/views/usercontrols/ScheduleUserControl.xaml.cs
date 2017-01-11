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
    public partial class ScheduleUserControl : UserControl
    {
        public ScheduleUserControl()
        {
            InitializeComponent();

            Schedule schedule = new Schedule();

            schedule.Start = DateTime.Now;
            schedule.End = DateTime.Now.AddDays(1.2);

            label1.Content = "Start";
            textBox1.Text = schedule.Start.ToString();

            label2.Content = "End";
            textBox2.Text = schedule.End.ToString();
        }
    }
}
