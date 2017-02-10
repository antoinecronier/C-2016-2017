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
using wpfzoo.entities.enums;
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
                currentZoo = (e.AddedItems[0] as Zoo);
                this.zooAdmin.ucZoo.Zoo = currentZoo;
                zooManager.GetAddress(currentZoo);
                zooManager.GetEmployees(currentZoo);
                zooManager.GetStructures(currentZoo);
            }
        }


        private void BtnAddress_Click(object sender, RoutedEventArgs e)
        {
            if (currentZoo.Id !=0)
            {
                this.zooAdmin.NavigationService.Navigate(new AddressAdmin(this));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("You must select a zoo to go to its address page... ", "No zoo selected",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }


#region address


        public void LoadAddressPage(AddressAdmin addressAdmin)
        {
            this.addressAdmin = addressAdmin;
            InitAddressPage();
            InitAddressLists();
            InitAddressUC();
            if (currentZoo.Address != null)
            {
                GetCurrentZooAddressFull();
                FillAddressUC();
            }
            
            InitAddressActions();
        }

        private void GetCurrentZooAddressFull()
        {
                MySQLAddressManager addressManager = new MySQLAddressManager();
                addressManager.GetStreetNumber(currentZoo.Address);
        }

        private void FillAddressUC()
        {
            if (currentZoo.Address.Id != 0)
            {
                this.addressAdmin.UCAddress.Address = currentZoo.Address;
                this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = currentZoo.Address.StreetNumber;
            }
        }

        private void InitAddressPage()
        {
            this.addressAdmin.btnNew.Visibility = Visibility.Hidden;
        }

        private void InitAddressActions()
        {
            this.addressAdmin.btnValidate.Click += BtnAddressValidate_Click;
            this.addressAdmin.btnDelete.Click += BtnAddressDelete_Click;
            this.addressAdmin.UCAddressList.ItemsList.SelectionChanged += AddressList_SelectionChanged;
        }

        private void AddressList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Address item = (e.AddedItems[0] as Address);

                MySQLAddressManager addressManager = new MySQLAddressManager();
                addressManager.GetStreetNumber(item);

                this.addressAdmin.UCAddress.Address = item;
                this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = item.StreetNumber;
            }
        }

        private async void BtnAddressDelete_Click(object sender, RoutedEventArgs e)
        {
            currentZoo.Address = this.addressAdmin.UCAddress.Address;

            if (currentZoo.Address.Id == 0)
            {
                System.Windows.MessageBox.Show("Cannot delete new element in database");
            }
            else
            {
                currentZoo.Address = null;
                InitAddressUC();
            }
        }

        private void BtnAddressValidate_Click(object sender, RoutedEventArgs e)
        {
            currentZoo.Address = this.addressAdmin.UCAddress.Address;
            this.addressAdmin.NavigationService.GoBack();
        }

        private void InitAddressUC()
        {
            this.addressAdmin.UCAddress.Address = new Address();
            this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = new StreetNumber();
            foreach (StreetAvaibleItems streetAvaibleItem in Enum.GetValues(typeof(StreetAvaibleItems)))
            {
                this.addressAdmin.UCAddress.UCStreetNumber.cbSuff.Items.Add(streetAvaibleItem);
            }
        }

        private async void InitAddressLists()
        {
            MySQLAddressManager addressManager = new MySQLAddressManager();
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
                    this.zooAdmin.ucZoo.Zoo = await zooManager.Insert(this.zooAdmin.ucZoo.Zoo);
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

