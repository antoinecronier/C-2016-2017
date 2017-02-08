using ClassLibrary2.Entities.Generator;
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
using wpfzoo.views.usercontrols;

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
            this.UCAnimal.Animal = new Animal();
            this.UCAnimalList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            InitLists();
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Animal item = (e.AddedItems[0] as Animal);
                this.UCAnimal.Animal = item;
            }
        }

        private async void InitLists()
        {
            MySQLManager<Animal> listAnimalManager = new MySQLManager<Animal>();
            this.UCAnimalList.LoadItem((await listAnimalManager.Get()).ToList());
        }

        private async void LinkItem()
        {
            MySQLManager<Animal> itemAnimalManager = new MySQLManager<Animal>();
            
        }

        private void ClickNew(object sender, RoutedEventArgs e)
        {

        }

        private void ClickDelete(object sender, RoutedEventArgs e)
        {
            MySQLManager<Animal> animalManager = new MySQLManager<Animal>();
            this.UCAnimalList.Obs.Remove(UCAnimalList.Animal);

        }

        private void ClickOK(object sender, RoutedEventArgs e)
        {
            MySQLManager<Animal> animalManager = new MySQLManager<Animal>();
            Task<Animal> tAnimal = animalManager.Insert(UCAnimal.Animal);
        }

        private void ClickAnimalList(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}

