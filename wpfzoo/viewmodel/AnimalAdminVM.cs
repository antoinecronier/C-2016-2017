using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void InitUC()
        {
            currentAnimal = new Animal();
            this.animalAdmin.UCAnimal.Animal = currentAnimal;
        }

        private void InitActions()
        {
            this.animalAdmin.btnOk.Click += BtnValidate_Click;
        }

        private void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            animalManager.Insert(this.animalAdmin.UCAnimal.Animal);
        }
    }
}
