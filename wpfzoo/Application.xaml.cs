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

namespace wpfzoo
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Application : Window
    {
        public Application()
        {
            InitializeComponent();
            Zoo zoo = new Zoo();
            Address add = new Address();
        }

        private void btnAddress_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Address");
        }
        private void btnAnimal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Animal");
        }
        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Employee");
        }
        private void btnJob_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Job");
        }
        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Schedule");
        }
        private void btnStreetNumber_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("StreetNumber");
        }
        private void btnStructure_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Structure");
        }
        private void btnZoo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Zoo");
        }
    }
}
