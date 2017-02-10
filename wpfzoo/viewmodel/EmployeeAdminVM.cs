using System;
using System.Windows;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using wpfzoo.entities.enums;
using System.Windows.Media;
using ClassLibrary2.Entities.Reflection;
using wpfzoo.database.entitieslinks;
using wpfzoo.views.usercontrols;

namespace wpfzoo.viewmodel
{
    public class EmployeeAdminVM
    {
        private const String RegexName = "^[a-zA-Z]+$-*"; //Work with method checkRegex()
        private readonly string[] ListName = {"Last name", "First name", "Manager last name", "Manager first name" }; //Work with method checkRegexTxtBName()
        List<TextBox> listTxtB = new List<TextBox>();
        private Employee currentEmployee;
        private Address currentAddress;
        private EmployeeAdmin employeeAdmin;
        private MySQLManager<Employee> employeeManager = new MySQLManager<Employee>();
        private MySQLManager<Address> addressManager = new MySQLManager<Address>();
        MySQLAddressManager mySqlAddressManager = new MySQLAddressManager();


        private AddressAdmin addressAdmin;

        #region employee

        public EmployeeAdminVM(EmployeeAdmin employeeAdmin)
        {
            this.employeeAdmin = employeeAdmin;
            InitUC();
            InitLUC();
            InitActions();
        }


        private async void MenuDuplicate_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.employeeAdmin.ucEmployeeList.itemList.SelectedItems.Count > 0)
            {
                Employee dupplicateEmployee = new Employee(this.employeeAdmin.ucEmployeeList.itemList.SelectedItem as Employee);
                await employeeManager.Insert(dupplicateEmployee);
                InitLUC();
            }
        }

        private async void MenuDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.employeeAdmin.ucEmployeeList.itemList.SelectedItems.Count > 0)
            {
                Employee deleteEmployee = this.employeeAdmin.ucEmployeeList.itemList.SelectedItem as Employee;
                await employeeManager.Delete(deleteEmployee);
                InitLUC();
            }
        }

        private async void btnDelEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkRegexTxtBName())
            {
                await employeeManager.Delete(this.employeeAdmin.ucEmployee.Employee);
                InitLUC();
                InitUC();
            }
        }

        public bool checkDateP()
        {
            DatePicker datePHiring = this.employeeAdmin.ucEmployee.DatePHiring;
            DatePicker datePBirth = this.employeeAdmin.ucEmployee.DatePBirth;

            datePHiring.Background = Brushes.White;
            datePBirth.Background = Brushes.White;

            int hiring = datePHiring.DisplayDate.Year;
            int birth = datePBirth.DisplayDate.Year;
            Console.WriteLine(hiring);
            Console.WriteLine(birth);
            int age = (hiring - birth);
            Console.WriteLine(age);
            if (datePHiring.DisplayDate > datePBirth.DisplayDate && age >= 18)
            {
                return true;
            }
            else
            {
                datePHiring.Background = Brushes.Red;
                datePBirth.Background = Brushes.Red;
                MessageBox.Show("Birth > Hiring or (Hiring - Birth) < 18");
                return false;
            }

        }

        public bool checkRegexTxtBName()
        {
            bool output = true;

            clearTextBBg(listTxtB);

            for (int index = 0; index < ListName.Length; index++)
            {
                if (!checkRegex(listTxtB.ElementAt(index), RegexName))
                {
                    if (index > 1 && !listTxtB.ElementAt(index).Text.Equals(""))
                    {
                        listTxtB.ElementAt(index).Background = Brushes.Red;
                        MessageBox.Show(ListName[index] + " is not valid.");
                        output = false;
                    }
                }
            }

            return output;
        }

        public void clearTextBBg(List<TextBox> listTextBoxs)
        {
            for (int index = 0; index < ListName.Length; index++)
            {
                listTextBoxs.ElementAt(index).Background = Brushes.White;
            }
        }

        private async void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee.Id != 0)
            {
                if (checkRegexTxtBName() && checkDateP())
                {
                    await employeeManager.Update(this.employeeAdmin.ucEmployee.Employee);
                    InitLUC();
                }
            }
            else
            {
                if (checkRegexTxtBName() && checkDateP())
                {
                    await employeeManager.Insert(this.employeeAdmin.ucEmployee.Employee);
                    InitLUC();
                }
            }       
        }

        private void btnNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.currentEmployee = new Employee();
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                currentEmployee = (e.AddedItems[0] as Employee);
                MySQLEmployeeManager mySqlEmployeeManager = new MySQLEmployeeManager();
                mySqlEmployeeManager.GetAddress(currentEmployee);
                this.employeeAdmin.ucEmployee.Employee = currentEmployee;
            }
        }

        private void InitUC()
        {
            currentEmployee = new Employee();
            currentEmployee.Birth = DateTime.Now;
            currentEmployee.Hiring = DateTime.Now;
            this.employeeAdmin.ucEmployee.Employee = currentEmployee;

            foreach (Gender gender in Enum.GetValues(typeof(Gender)))
            {
                this.employeeAdmin.ucEmployee.cboCGender.Items.Add(gender);
            }

            listTxtB.Clear();
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBLastname);
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBFirstname);
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBManagerLastname);
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBManagerFirstname);
        }

        private bool checkRegex(TextBox valueTested, String regex)
        {
            Match match = Regex.Match(valueTested.Text, regex);

            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private async void InitLUC()
        {
            this.employeeAdmin.ucEmployeeList.LoadItem((await employeeManager.Get()).ToList());
        }

        private void InitActions()
        {
            this.employeeAdmin.ucEmployee.btnAddress.Click += BtnAddress_Click;
            this.employeeAdmin.btnNewEmployee.Click += btnNewEmployee_Click;
            this.employeeAdmin.btnSaveEmployee.Click += btnSaveEmployee_Click;
            this.employeeAdmin.btnDelEmployee.Click += btnDelEmployee_Click;
            this.employeeAdmin.menuDuplicate.Click += MenuDuplicate_OnClick;
            this.employeeAdmin.menuDelete.Click += MenuDelete_OnClick;
            this.employeeAdmin.ucEmployeeList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        public void LoadAddressPage(AddressAdmin addressAdmin)
        {
            this.addressAdmin = addressAdmin;
            InitLUCAddress();
            ResetAddress();
            InitActionsAddress();
            if (currentEmployee.Address != null)
            {
                currentAddress = currentEmployee.Address;
                mySqlAddressManager.GetStreetNumber(currentAddress);
                this.addressAdmin.UCAddress.Address = currentEmployee.Address;
                this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = currentEmployee.Address.StreetNumber;

            }
        }
        #endregion
        #region address
        #region validate
        private async void BtnValidateAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            //MySQLManager<StreetNumber> snManager = new MySQLManager<StreetNumber>();
            if (currentAddress.Id != 0)
            {
                await addressManager.Update(currentAddress);
                this.addressAdmin.NavigationService.GoBack();
            }
            else
            {
                try
                {
                    await addressManager.Insert(currentAddress);
                    this.addressAdmin.UCAddressList.AddItem(currentAddress);
                    this.addressAdmin.NavigationService.GoBack();
                }
                catch (Exception)
                {
                    MessageBox.Show("One or more fields are not valid.");
                }
                //await snManager.Insert(currentAddress.StreetNumber);

            }
        }
        #endregion

        #region delete
        private void BtnDeleteAddress_Click(object sender, RoutedEventArgs e)
        {
            currentAddress = this.addressAdmin.UCAddress.Address;

            if (currentAddress.Id == 0)
            {
                MessageBox.Show("Cannot delete new element in database");
            }
            else
            {
                confirmDelete();
            }
        }
    #endregion 

        #region utils
        private void ResetAddress()
        {
            currentAddress = new Address(new StreetNumber());
            this.addressAdmin.UCAddress.Address = currentAddress;
            this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = currentAddress.StreetNumber;

            //this.addressAdmin.UCAddress.UCStreetNumber.txtBSuf.ItemsPanel = currentAddress.StreetNumber.Suf;
        }

        private async void confirmDelete()
        {
            MessageBoxResult mbr = MessageBox.Show("Do you really want to delete this item ?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

            if (mbr == MessageBoxResult.OK)
            {
                await addressManager.Delete(currentAddress);
                this.addressAdmin.UCAddressList.RemoveItem(currentAddress);
                this.ResetAddress();
            }
        }
        #endregion

        private void RemoveAddressContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            //Address itemToDelete = this.addressAdmin.UCAddressList.ItemsList.SelectedItem as Address;
            confirmDelete();
        }

        private async void DuplicateAddressContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.addressAdmin.UCAddressList.ItemsList.SelectedIndex > -1)
            {
                var address = new Address();
                address = (Address)this.addressAdmin.UCAddressList.ItemsList.SelectedItem; // casting the list view 
                await addressManager.Insert(address);
                this.addressAdmin.UCAddressList.LoadItems((await addressManager.Get()).ToList());
            }

        }

        private void InitActionsAddress()
        {
            this.addressAdmin.btnValidate.Click += BtnValidateAddress_Click;
            this.addressAdmin.btnNew.Click += BtnNewAddress_Click;
            this.addressAdmin.btnDelete.Click += BtnDeleteAddress_Click;
            this.addressAdmin.UCAddressList.ItemsList.SelectionChanged += ItemsListAddress_SelectionChanged;
            this.addressAdmin.UCAddressList.RemoveAddressContextMenu.Click += RemoveAddressContextMenu_OnClick;
            this.addressAdmin.UCAddressList.DuplicateAddressContextMenu.Click += DuplicateAddressContextMenu_OnClick;

        }

        private void ItemsListAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Address item = (e.AddedItems[0] as Address);
                currentAddress = item;
                this.addressAdmin.UCAddress.Address = currentAddress;
                mySqlAddressManager.GetStreetNumber(currentAddress);
                this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = currentAddress.StreetNumber;
            }
        }


        private async void BtnNewAddress_Click(object sender, RoutedEventArgs e)
        {
            addressManager.DbSetT.Attach(currentAddress);
            Address loadedAddress = await addressManager.Get(currentAddress.Id);
            currentAddress = this.addressAdmin.UCAddress.Address;

            // Check if we have filled props
            Reflectionner reflec = new Reflectionner();
            Boolean areFieldsdEmpty = true;
            var dico = reflec.ReadObject<Address>(currentAddress);
            Dictionary<String, Object> dico2 = null;

            if (loadedAddress != null)
            {
                dico2 = reflec.ReadObject<Address>(loadedAddress);
            }

            if (dico["Id"].Equals(0))
            {
                dico.Remove("Id");

                foreach (var item in dico)
                {
                    if (item.Value != null)
                    {
                        areFieldsdEmpty = false;
                        break;
                    }

                }
            }
            else //Fields not empty, but entity loaded from db
            {

                foreach (var item in dico)
                {
                    if (item.Key != "Id" & item.Value != dico2[item.Key])
                    {
                        areFieldsdEmpty = false;
                        break;
                    }
                }
            }


            if (!areFieldsdEmpty)
            {
                MessageBoxResult mbr = MessageBox.Show("You have filled some data. Do you want to wipe them all ? (cannot be undone)", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

                if (mbr == MessageBoxResult.OK)
                {
                    this.ResetAddress();
                }
            }
            else
            {
                this.ResetAddress();
            }

        }

        private async void InitLUCAddress()
        {
            this.addressAdmin.UCAddressList.LoadItems((await addressManager.Get()).ToList());
        }

        private void BtnAddress_Click(object sender, RoutedEventArgs e)
        {
            this.employeeAdmin.NavigationService.Navigate(new AddressAdmin(this));
        }

        #endregion

    }
}
