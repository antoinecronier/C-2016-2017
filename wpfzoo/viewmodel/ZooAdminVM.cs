using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ZooAdminVM
    {
        private Zoo currentZoo;
        private ZooAdmin zooAdmin;
        private MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
        private StructureAdmin structureAdmin;


        public object UCZooList { get; private set; }

        public ZooAdminVM(ZooAdmin zooAdmin)
        {
            this.zooAdmin = zooAdmin;

            InitUC();
            InitLUC();
            InitActions();
        }

        private async void InitLUC()
        {
            this.zooAdmin.UCZooList.LoadItem((await zooManager.Get()).ToList());
        }

        private void InitUC()
        {
            currentZoo = new Zoo();
            currentZoo.Birth = DateTime.Now;
            this.zooAdmin.ucZoo.Zoo = currentZoo;
        }

        private void InitActions()
        {
            this.zooAdmin.btnValidateZoo.Click += BtnValidate_Click;
            this.zooAdmin.btnDelZoo.Click += BtnDel_Click;
            this.zooAdmin.btnNewZoo.Click += BtnNew_Click;
            this.zooAdmin.UCZooList.DuplicateZooContextMenu.Click += DuplicateZoo_Click;
            this.zooAdmin.UCZooList.RemoveZooContextMenu.Click += BtnDel_Click;
            this.zooAdmin.UCZooList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            this.zooAdmin.btnStructure.Click += BtnStructure_Click; 
        }

        private void BtnStructure_Click(object sender, RoutedEventArgs e)
        {

        }

        public void LoadStructurePage(StructureAdmin structureAdmin)
        {
            this.structureAdmin = structureAdmin;
        }

        private void DuplicateZoo_Click(object sender, RoutedEventArgs e)
        {
            Zoo zoo = new entities.Zoo();
            zoo.Birth = this.zooAdmin.ucZoo.Zoo.Birth;
            zoo.Address = this.zooAdmin.ucZoo.Zoo.Address;
            zoo.Name = this.zooAdmin.ucZoo.Zoo.Name;
            zoo.Staff = this.zooAdmin.ucZoo.Zoo.Staff;
            zoo.Structures = this.zooAdmin.ucZoo.Zoo.Structures;

            Task <Zoo> tZoo = zooManager.Insert(zoo);
            Zoo zooRes = (Zoo)tZoo.Result;
            this.zooAdmin.ucZoo.Zoo = zooRes;
            InitLUC();
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Zoo item = (e.AddedItems[0] as Zoo);
                this.zooAdmin.ucZoo.Zoo = item;
            }
        }

        private async void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.zooAdmin.ucZoo.Zoo.Birth < DateTime.Now)
            {
                if (this.zooAdmin.ucZoo.Zoo.Id != 0)
                {
                    await zooManager.Update(this.zooAdmin.ucZoo.Zoo);
                }
                else
                {
                    Task<Zoo> tZoo = zooManager.Insert(this.zooAdmin.ucZoo.Zoo);
                    Zoo zoo = (Zoo)tZoo.Result;
                    this.zooAdmin.ucZoo.Zoo = zoo;
                    InitLUC();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please do not create a zoo in the future... ", "Delete Zoo",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                currentZoo.Birth = DateTime.Now;
            }
            
        }

        private async void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (this.zooAdmin.ucZoo.Zoo.Id != 0)
            {
                if (System.Windows.Forms.MessageBox.Show("Do you want to delete " + this.zooAdmin.ucZoo.Zoo.Name + " Zoo?", "Delete Zoo",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                 == DialogResult.Yes)
                {
                    this.zooAdmin.UCZooList.Obs.Remove(zooAdmin.ucZoo.Zoo);
                    await zooManager.Delete(zooAdmin.ucZoo.Zoo);
                    currentZoo = new Zoo();
                    this.zooAdmin.ucZoo.Zoo = currentZoo;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("You must select an object to delete it... ", "Delete Zoo",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            InitUC();
        }
    }
}

