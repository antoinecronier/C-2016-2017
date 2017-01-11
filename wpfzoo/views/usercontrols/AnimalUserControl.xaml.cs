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
    /// Logique d'interaction pour AnimalUserControl.xaml
    /// </summary>
    public partial class AnimalUserControl : UserControl
    {
        public AnimalUserControl()
        {
            InitializeComponent();
            Animal animal = new Animal();
            animal.Name = "moufassa";
            animal.Gender = Gender.MALE;
            animal.Birth = DateTime.Now;
            animal.Height = 1.2M;
            animal.Weight = 120;
            animal.IsDead = false;

            this.label.Content = "Name";
            this.textBox.Text = animal.Name;

            this.label1.Content = "Gender";
            this.textBox1.Text = animal.Gender.ToString();

            this.label2.Content = "Birth";
            this.textBox2.Text = animal.Birth.ToString();

            this.label3.Content = "Height";
            this.textBox3.Text = animal.Height.ToString();

            this.label4.Content = "Weight";
            this.textBox4.Text = animal.Weight.ToString();

            this.label5.Content = "Is Dead";
            this.textBox5.Text = animal.IsDead.ToString();
        }
    }
}
