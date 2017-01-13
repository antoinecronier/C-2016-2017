using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using wpfzoo.database;
using wpfzoo.entities;

namespace wpfzoo.views.adminstration
{

    /// <summary>
    /// Logique d'interaction pour AnimalAdmin.xaml
    /// </summary>
    public partial class AnimalAdmin : Page
    {
        public AnimalAdmin()
        {
            InitializeComponent();
            MySQLManager<Animal> managerAnimal = new MySQLManager<Animal>();
        }

        private void ClickNew(object sender, RoutedEventArgs e)
        {
            
        }

        private void ClickDelete(object sender, RoutedEventArgs e)
        {
        
        }

        private void ClickOK(object sender, RoutedEventArgs e)
        {
        
        }


    }
}

