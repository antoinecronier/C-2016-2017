using System.Linq;
using System.Windows;
using System.Windows.Controls;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;

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
                MessageBox.Show("You have deleted an animal");
            }
        }

        private async void ClickOK(object sender, RoutedEventArgs e)
        {
            if (this.animalAdmin.UCAnimal.Animal.Id != 0)
            {
                if (this.animalAdmin.UCAnimal.Animal.Weight > 0)
                {
                    if (animalAdmin.UCAnimal.Animal.IsDead == true || animalAdmin.UCAnimal.Animal.IsDead == false)
                    {
                        await animalManager.Update(this.animalAdmin.UCAnimal.Animal);
                        MessageBox.Show("You have update a animal");
                    }
                    else
                    {
                        MessageBox.Show("Enter isDead : False or True");
                    }
                }
                else
                {
                    MessageBox.Show("Enter weight > 0");
                }
            }
            else
            {
                if (this.animalAdmin.UCAnimal.Animal.Weight > 0)
                {
                    if (animalAdmin.UCAnimal.Animal.IsDead == true || animalAdmin.UCAnimal.Animal.IsDead == false)
                    {
                        await animalManager.Insert(this.animalAdmin.UCAnimal.Animal);
                        MessageBox.Show("You have create a new animal");
                    }
                    else
                    {
                        MessageBox.Show("Enter isDead : False or True");
                    }
                }
                else
                {
                   MessageBox.Show("Enter weight > 0");
                }
            }
            InitLists();
        }

        private async void ClickMenuDupli(object sender, RoutedEventArgs e)
        {
            if (this.animalAdmin.UCAnimal.Animal.Id != 0)
            {
                this.animalAdmin.UCAnimal.Animal.Id = 0;
                await animalManager.Insert(this.animalAdmin.UCAnimal.Animal);
                InitLists();
            }
        }

        private async void ClickMenuDel(object sender, RoutedEventArgs e)
        {
            await animalManager.Delete(this.animalAdmin.UCAnimal.Animal);
            this.animalAdmin.UCAnimal.Animal = new Animal();
            InitLists();
        }
    }
}