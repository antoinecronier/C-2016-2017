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
using wpfzoo.database.entitieslinks;
using wpfzoo.entities;
using wpfzoo.views.administration;

namespace wpfzoo.viewmodel
{
    public class ZooAdminVM
    {
        private Zoo currentZoo;
        private ZooAdmin zooAdmin;
        private MySQLZooManager zooManager = new MySQLZooManager();
        private AddressAdmin addressAdmin;

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
            this.zooAdmin.btnAddress.Click += BtnAddress_Click;
            this.zooAdmin.btnEmployee.Click += BtnEmployees_Click;
            this.zooAdmin.btnStructure.Click += BtnStructures_Click;
            this.zooAdmin.UCZooList.DuplicateZooContextMenu.Click += DuplicateZoo_Click;
            this.zooAdmin.UCZooList.RemoveZooContextMenu.Click += BtnDel_Click;
            this.zooAdmin.UCZooList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
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
                //MySQLZooManager mySqlZooManager = new MySQLZooManager();
                zooManager.GetAddress(currentZoo);
                zooManager.GetEmployees(currentZoo);
                zooManager.GetStructures(currentZoo);
            }
        }

#region address

        

        private void BtnAddress_Click(object sender, RoutedEventArgs e)
        {
                this.zooAdmin.NavigationService.Navigate(new AddressAdmin(this));
        }



        public void LoadAddressPage(AddressAdmin addressAdmin)
        {
            this.addressAdmin = addressAdmin;
            InitAddressLists();
            InitAddressUC();
            InitAddressActions();
        }

        private void InitAddressActions()
        {
            this.addressAdmin.btnValidate.Click += BtnAddressValidate_Click;
            this.addressAdmin.btnNew.Click += BtnAddressNew_Click;
            this.addressAdmin.btnDelete.Click += BtnAddressDelete_Click;
            this.addressAdmin.UCAddressList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        private void InitAddressUC()
        {
            this.addressAdmin.UCAddress.Address = this.currentZoo.Address;
        }

        private void InitAddressLists()
        {
            this.addressAdmin.UCAddressList.LoadItems((await addressManager.Get()).ToList());
        }

        #endregion //address

        private void BtnStructures_Click(object sender, RoutedEventArgs e)
        {
            if (currentZoo.Structures.Count > 0)
            {
                StructureAdmin structureAdmin = new StructureAdmin();
                Window window = new Window();
                window.Content = structureAdmin;
                window.Show();
                structureAdmin.UCstructureList.LoadItem(currentZoo.Structures);
            }
            else
            {
                StructureAdmin structureAdmin = new StructureAdmin();
                Window window = new Window();
                window.Content = structureAdmin;
                window.Show();
            }
        }

        private void BtnEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (currentZoo.Staff.Count > 0)
            {
                EmployeeAdmin employeeAdmin = new EmployeeAdmin();
                Window window = new Window();
                window.Content = employeeAdmin;
                window.Show();
                employeeAdmin.ucEmployeeList.LoadItem(currentZoo.Staff);
            }
            else
            {
                EmployeeAdmin employeeAdmin = new EmployeeAdmin();
                Window window = new Window();
                window.Content = employeeAdmin;
                window.Show();
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

