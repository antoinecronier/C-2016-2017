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
using wpfzoo.entities.enums;

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Logique d'interaction pour EmployeeUserControl.xaml
    /// </summary>
    public partial class EmployeeUserControl : UserControl
    {
        public EmployeeUserControl()
        {
            InitializeComponent();
            Employee employer = new Employee();
            Job job = new Job();
            List<Job> jobs = new List<Job>();
            Employee manager = new Employee();
            Dictionary<Schedule, Structure> planning = new Dictionary<Schedule,Structure>();


            employer.Firstname = "François";
            employer.Lastname = "Bash";
            employer.Gender = Gender.MALE;
            employer.Birth = DateTime.Now;
            employer.Hiring = DateTime.Now;
            employer.Jobs = jobs;
            employer.Manager = manager;
            employer.Planning = planning;

            label.Content = "Firstname";
            textBox.Text = employer.Firstname;

            label1.Content = "Lastname";
            textBox1.Text = employer.Lastname.ToString();

            label2.Content = "Gender";
            textBox2.Text = employer.Gender.ToString();

            label3.Content = "Hiring";
            textBox3.Text = employer.Birth.ToString();
        }
    }
}
