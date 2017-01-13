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
using wpfzoo.views;
using wpfzoo.views.administration;

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
            Window window = new Window();
            //window.Content = new AddressAdmin();

            window.Show();
        }
        private void btnAnimal_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            //window.Content = new AnimalAdmin();

            window.Show();
        }
        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            //window.Content = new EmployeeAdmin();

            window.Show();
        }
        private void btnJob_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            //window.Content = new JobAdmin();

            window.Show();
        }
        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            //window.Content = new ScheduleAdmin();

            window.Show();
        }
        private void btnStreetNumber_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            //window.Content = new StreetNumberAdmin();

            window.Show();
        }
        private void btnStructure_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            //window.Content = new StructureAdmin();

            window.Show();
        }
        private void btnZoo_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Content = new ZooAdmin();

            window.Show();
        }
    }
}
