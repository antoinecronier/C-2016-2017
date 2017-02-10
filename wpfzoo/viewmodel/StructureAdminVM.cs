using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;

namespace wpfzoo.viewmodel
{
    public class StructureAdminVM
    {
        private Structure currentStructure;
        private StructureAdmin structureAdmin;
        private MySQLManager<Structure> structureManager = new MySQLManager<Structure>();


        public StructureAdminVM(StructureAdmin structureAdmin)
        {
            this.structureAdmin = structureAdmin;
            structureAdmin.UCstructureList.itemList.SelectionChanged += structureAdmin_ListSelectionChanged;

            InitUC();
            ClicksGenerator();
        }
        private void InitUC()
        {
            currentStructure = new Structure();
            this.structureAdmin.ucStructure.Structure = currentStructure;
        }

        private void structureAdmin_ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Structure item = (e.AddedItems[0] as Structure);
                this.structureAdmin.ucStructure.Structure = item;
            }
        }
        
        private void ClicksGenerator()
        {
            this.structureAdmin.buttonNew.Click += BtnValidate_Click;
            this.structureAdmin.Ok.Click += BtnUpdate_Click;
            this.structureAdmin.Delete.Click += BtnDelete_Click;
            this.structureAdmin.ucStructure.buttonEmploye.Click += ButtonEmploye_Click;
            this.structureAdmin.ucStructure.buttonAnimaux.Click += ButtonAnimaux_Click;
            this.structureAdmin.ucStructure.buttonSchedule.Click += ButtonSchedule_Click;
        }
        #region Animaux
        private void ButtonAnimaux_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            AnimalAdmin newAnimalAdmin = new AnimalAdmin();
            //AnimalAdminSecondColumnGenerator(newAnimalAdmin);
            window.Content = newAnimalAdmin;
            window.Show();
        }

        private void AnimalAdminSecondColumnGenerator(AnimalAdmin newAnimalAdmin)
        {
            //System.Windows.Controls.ListView newListView = new System.Windows.Controls.ListView();
            //newListView.ItemsSource = this.structureAdmin.ucStructure.Structure.AssignAnimals;
            //newAnimalAdmin.UCAnimalList.mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
           // newAnimalAdmin.UCAnimalList.mainGrid.Children.
        }
        #endregion

        private void ButtonEmploye_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Content = new EmployeeAdmin();
            window.Show();
        }

        #region ScheduleAdmin
        private void ButtonSchedule_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Content = new ScheduleAdmin();
            window.Show();
        }
        #endregion

        #region Btn
        private void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.structureAdmin.ucStructure.Structure = new Structure();
            this.structureAdmin.ucStructure.txtBSurface.Text = "0";
            this.structureAdmin.ucStructure.txtBName.Text = "";
        }

        private void Ajout()
        {

            if (!this.structureAdmin.ucStructure.txtBSurface.Text.Contains("-"))
            {
                int test;
                bool isParsable = Int32.TryParse(this.structureAdmin.ucStructure.txtBSurface.Text, out test);
                if (isParsable)
                {
                    if (test > 0)
                    {
                        structureManager.Insert(this.structureAdmin.ucStructure.Structure);
                        structureAdmin.UCstructureList.Obs.Add(this.structureAdmin.ucStructure.Structure);
                        this.structureAdmin.ucStructure.Structure = new Structure();
                    }
                    else
                        System.Windows.MessageBox.Show("Surface doit être > à 0 ... biche");
                }
                else
                {
                    System.Windows.MessageBox.Show("Veuillez n'entrer que des chiffres");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Erreur surface négative wsh");
            }
        }

        private void BtnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Il faut que le nom ne soit pas vide
            if (!String.IsNullOrWhiteSpace(this.structureAdmin.ucStructure.txtBName.Text))
            {
                // Si la structure n'est pas trouvé dans la Db (n'existe donc pas) on en créer une nouvelle avec la fonction "Ajout"
                if (this.structureAdmin.ucStructure.Structure.Id == 0)
                {
                    Ajout();
                }
                else
                {
                    // Si elle existe alors on test que la textBox de la surface ne contient pas de caractère négatif(éviter une surface négative)
                    if (!this.structureAdmin.ucStructure.txtBSurface.Text.Contains("-"))
                    {
                        //test si le contenu la textBox de la surface ne contient que des chiffres
                        int test;
                        bool isParsable = Int32.TryParse(this.structureAdmin.ucStructure.txtBSurface.Text, out test);
                        if (isParsable && test > 0)
                        {
                            // Si le contenu de la textBox ne contient que des chiffres on test si elle est bien > à 0
                            if (test > 0)
                            {
                                structureManager.Update(this.structureAdmin.ucStructure.Structure);
                                Structure item = structureAdmin.UCstructureList.Obs.FirstOrDefault(i => i.Id == this.structureAdmin.ucStructure.Structure.Id);
                                item = this.structureAdmin.ucStructure.Structure;
                                structureAdmin.UCstructureList.itemList.SelectedItem = null;
                                this.structureAdmin.ucStructure.Structure = new Structure();
                            }
                            else
                                System.Windows.MessageBox.Show("Surface doit être > à 0 ... biche");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Veuillez n'insérer que des chiffres");
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Erreur surface négative wsh");
                    }
                }
            }
            else
                System.Windows.MessageBox.Show("Veuillez entrer un nom");
        }

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            structureManager.Delete(this.structureAdmin.ucStructure.Structure);
            structureAdmin.UCstructureList.Obs.Remove(this.structureAdmin.ucStructure.Structure);
            this.structureAdmin.ucStructure.txtBSurface.Text = "0";
            this.structureAdmin.ucStructure.txtBName.Text = "";
        }
        #endregion


    }
}
