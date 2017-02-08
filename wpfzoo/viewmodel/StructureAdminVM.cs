using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            InitUC();
        }

        private void InitUC()
        {
            currentStructure = new Structure();
            this.structureAdmin.ucStructure.Structure = currentStructure;
        }

        private void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            structureManager.Insert(this.structureAdmin.ucStructure.Structure);
        }

        private void Update()
        {
            this.structureAdmin.Ok.Click += BtnUpdate_Click;
        }

        private void BtnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            structureManager.Update(this.structureAdmin.ucStructure.Structure);
        }

        private void Delete()
        {
            this.structureAdmin.Delete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            structureManager.Delete(this.structureAdmin.ucStructure.Structure);
        }
    }
}
