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
    /// Logique d'interaction pour StructureUserControl.xaml
    /// </summary>
    public partial class StructureUserControl : UserControl
    {
        public StructureUserControl()
        {
            InitializeComponent();

            Structure structure = new entities.Structure();

            structure.Name = "cage";
            structure.Surface = 1000;
            structure.AssignAnimals = new List<Animal>();
            structure.AssignEmployees = new List<Employee>();
            structure.Schedule = new Schedule();

            this.label0.Content = "Name";
            this.textBox0.Text = structure.Name;

            this.label1.Content = "Surface";
            this.textBox1.Text = structure.Surface.ToString();

            this.label2.Content = "AssignAnimals";
            this.textBox2.Text = structure.AssignAnimals.ToString();

            this.label3.Content = "AssignEmployees";
            this.textBox3.Text = structure.AssignEmployees.ToString();

            this.label4.Content = "Schedule";
            this.textBox4.Text = structure.Schedule.ToString();
        }
    }
}
