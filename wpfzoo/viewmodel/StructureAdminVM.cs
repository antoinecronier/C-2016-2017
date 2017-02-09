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

        private void structureAdmin_ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Structure item = (e.AddedItems[0] as Structure);
                this.structureAdmin.ucStructure.Structure = item;
            }
        }

        private void InitUC()
        {
            currentStructure = new Structure();
            this.structureAdmin.ucStructure.Structure = currentStructure;
        }

        private void ClicksGenerator()
        {
            this.structureAdmin.buttonNew.Click += BtnValidate_Click;
            this.structureAdmin.Ok.Click += BtnUpdate_Click;
            this.structureAdmin.Delete.Click += BtnDelete_Click;
        }

        private void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!this.structureAdmin.ucStructure.txtBSurface.Text.Contains("-"))
            {
                structureManager.Insert(this.structureAdmin.ucStructure.Structure);
                structureAdmin.UCstructureList.Obs.Add(this.structureAdmin.ucStructure.Structure);
                this.structureAdmin.ucStructure.txtBSurface.Text = "";
                this.structureAdmin.ucStructure.txtBName.Text = "";
            }
            else
            {
                System.Windows.MessageBox.Show("Erreur surface négative wsh");
            }
        }

        private void BtnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!this.structureAdmin.ucStructure.txtBSurface.Text.Contains("-"))
            {
                structureManager.Update(this.structureAdmin.ucStructure.Structure);
            Structure item = structureAdmin.UCstructureList.Obs.FirstOrDefault(i => i.Id == this.structureAdmin.ucStructure.Structure.Id);
            item = this.structureAdmin.ucStructure.Structure;
        }
            else
            {
                System.Windows.MessageBox.Show("Erreur surface négative wsh");
            }
}

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            structureManager.Delete(this.structureAdmin.ucStructure.Structure);
            structureAdmin.UCstructureList.Obs.Remove(this.structureAdmin.ucStructure.Structure);
            this.structureAdmin.ucStructure.txtBSurface.Text = "";
            this.structureAdmin.ucStructure.txtBName.Text = "";
        }

    }
}
