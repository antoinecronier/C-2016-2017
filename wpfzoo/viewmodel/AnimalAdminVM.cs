using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.adminstration;

namespace wpfzoo.viewmodel
{
    class AnimalAdminVM
    {
        private Animal currentAnimal;
        private AnimalAdmin animalAdmin;
        private MySQLManager<Animal> animalManager = new MySQLManager<Animal>();

  

        public AnimalAdminVM(AnimalAdmin animalAdmin)
        {
            this.animalAdmin = animalAdmin;

            InitUC();
            InitActions();
            this.animalAdmin.UCAnimal.Animal = new Animal();
            this.animalAdmin.UCAnimalList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            InitLists();
        }

        private void InitUC()
        {
            currentAnimal = new Animal();
            this.animalAdmin.UCAnimal.Animal = currentAnimal;
        }

        private void InitActions()
        {
            this.animalAdmin.btnOk.Click += ClickOK;
            this.animalAdmin.btnDelete.Click += ClickDelete;
            this.animalAdmin.btnNew.Click += ClickNew;
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Animal item = (e.AddedItems[0] as Animal);
                this.animalAdmin.UCAnimal.Animal = item;
            }
        }

        private async void InitLists()
        {
            this.animalAdmin.UCAnimalList.LoadItem((await animalManager.Get()).ToList());
        }

        private void ClickNew(object sender, RoutedEventArgs e)
        {
            this.animalAdmin.UCAnimal.Animal = new Animal();
        }

        private async void ClickDelete(object sender, RoutedEventArgs e)
        {
            if (this.animalAdmin.UCAnimal.Animal.Id != 0)
            {
                await animalManager.Delete(this.animalAdmin.UCAnimal.Animal);
                this.animalAdmin.UCAnimal.Animal = new Animal();
                InitLists();
                MessageBox.Show("You have deleted a animal");
            }
        }

        private async void ClickOK(object sender, RoutedEventArgs e)
        {
            if (this.animalAdmin.UCAnimal.Animal.Id != 0)
            {
                await animalManager.Update(this.animalAdmin.UCAnimal.Animal);
                MessageBox.Show("You have update a animal");
            }
            else
            {
                await animalManager.Insert(this.animalAdmin.UCAnimal.Animal);
                MessageBox.Show("You have create a new animal");
            }
            InitLists();
        }
    }
}













/* public AnimalAdmin()
        {
            InitializeComponent();
            this.animalAdmin.UCAnimal.Animal = new Animal();
            this.animalAdmin.UCAnimalList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            InitLists();
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Animal item = (e.AddedItems[0] as Animal);
                this.animalAdmin.UCAnimal.Animal = item;
            }
        }

        private async void InitLists()
        {
            this.animalAdmin.UCAnimalList.LoadItem((await animalManager.Get()).ToList());
        }

        private void ClickNew(object sender, RoutedEventArgs e)
        {
            this.animalAdmin.UCAnimal.Animal = new Animal();
        }

        private async void ClickDelete(object sender, RoutedEventArgs e)
        {
            if (this.animalAdmin.UCAnimal.Animal.Id != 0)
            {
                await animalManager.Delete(this.animalAdmin.UCAnimal.Animal);
                InitLists();
            }
        }

        private async void ClickOK(object sender, RoutedEventArgs e)
        {
            if (this.animalAdmin.UCAnimal.Animal.Id != 0) 
            {
                await animalManager.Update(this.animalAdmin.UCAnimal.Animal);

            }
            else
            {
                await animalManager.Insert(this.animalAdmin.UCAnimal.Animal);
                
            }
            InitLists();
        }
    }
}

*/