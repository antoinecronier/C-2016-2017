using ClassLibrary2.Entities.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using wpfzoo.database;
using wpfzoo.database.entitieslinks;
using wpfzoo.entities;
using wpfzoo.entities.enums;
using wpfzoo.entities.validator;
using wpfzoo.views.administration;

namespace wpfzoo.viewmodel
{
    class AddressAdminVM
    {
        #region attributes
        private Address currentAddress;
        private AddressAdmin addressAdmin;
        private MySQLAddressManager addressManager = new MySQLAddressManager();
        private Brush defaultColor; 
        #endregion

        #region ctor
        public AddressAdminVM(AddressAdmin addressAdmin)
        {
            this.addressAdmin = addressAdmin;
            InitLists();
            InitUC();
            InitActions();
        }
        #endregion

        #region Init methods
        private async void InitLists()
        {
            this.addressAdmin.UCAddressList.LoadItems((await addressManager.Get()).ToList());
        }

        private void InitUC()
        {
            this.ResetAddress();
            defaultColor = this.addressAdmin.UCAddress.txtBPostalCode.BorderBrush;
            foreach (var item in Enum.GetValues(typeof(StreetAvaibleItems)))
            {
                this.addressAdmin.UCAddress.UCStreetNumber.cbSuff.Items.Add(item);
            }
        }

        private void InitActions()
        {
            this.addressAdmin.btnValidate.Click += BtnValidate_Click;
            this.addressAdmin.btnNew.Click += BtnNew_Click;
            this.addressAdmin.btnDelete.Click += BtnDelete_Click;
            this.addressAdmin.UCAddress.txtBPostalCode.TextChanged += ValidatePostalCode;
            this.addressAdmin.UCAddressList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            this.addressAdmin.UCAddressList.RemoveAddressContextMenu.Click += RemoveAddressContextMenu_OnClick;
            this.addressAdmin.UCAddressList.DuplicateAddressContextMenu.Click += DuplicateAddressContextMenu_OnClick;
            //For validation
            //https://msdn.microsoft.com/en-us/library/cc488527.aspx
        }
        #endregion

        #region events methods
        #region List
        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Address item = (e.AddedItems[0] as Address);
                currentAddress = item;
                addressManager.GetStreetNumber(currentAddress);
                this.addressAdmin.UCAddress.Address = currentAddress;
                this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = currentAddress.StreetNumber;
            }
        }
        #endregion

        #region buttons
        #region save
        private async void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentAddress = this.addressAdmin.UCAddress.Address;

            
            //MySQLManager<StreetNumber> snManager = new MySQLManager<StreetNumber>();

            if (currentAddress.Id != 0)
            {
                try
                {
                    await addressManager.Update(currentAddress);
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("One or more fields are not valid.");
                    Console.WriteLine(e);
                    
                }
                
            }
            else
            {
                try
                {
                    await addressManager.Insert(currentAddress);
                    this.addressAdmin.UCAddressList.AddItem(currentAddress);
                }
                catch (Exception)
                {
                    MessageBox.Show("One or more fields are not valid.");
                }
                //await snManager.Insert(currentAddress.StreetNumber);
                
            }
        }
        #endregion

        #region new
        private async void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    throw new NotImplementedException();
            //}
            //catch (NotImplementedException n)
            //{
            //    MessageBox.Show("Partial implementation. Dev need to sleep. And sleeping is a proof of weakness, I know.");
            //}

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
        #endregion

        #region delete
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
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
        #endregion

        #region context menu
        private void RemoveAddressContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            //Address itemToDelete = this.addressAdmin.UCAddressList.ItemsList.SelectedItem as Address;
            confirmDelete();
        }

        private async void DuplicateAddressContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.addressAdmin.UCAddressList.ItemsList.SelectedIndex > -1)
            {
                //var address = new Address(new StreetNumber());
                //address = (Address)this.addressAdmin.UCAddressList.ItemsList.SelectedItem;
                //await addressManager.Insert(address);
                //this.addressAdmin.UCAddressList.LoadItems((await addressManager.Get()).ToList());

                
                var address = new Address(new StreetNumber());
                address = (Address)this.addressAdmin.UCAddressList.ItemsList.SelectedItem;
                addressManager.GetStreetNumber(address);
                var streetNumber = new StreetNumber();
                streetNumber.Number = address.StreetNumber.Number;
                streetNumber.Suf = address.StreetNumber.Suf;
                address.StreetNumber = streetNumber;
                await addressManager.Insert(address);
                this.addressAdmin.UCAddressList.LoadItems((await addressManager.Get()).ToList());

                
            }

        }
        #endregion
        #endregion

        #region utils
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

        private void ResetAddress()
        {
            currentAddress = new Address(new StreetNumber());
            this.addressAdmin.UCAddress.Address = currentAddress;
            this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = currentAddress.StreetNumber;
        }

        private void ValidatePostalCode(object sender, System.Windows.RoutedEventArgs e)
        {
            currentAddress = this.addressAdmin.UCAddress.Address;

            try
            {
                AddressValidator.Validate(currentAddress);

                this.addressAdmin.UCAddress.txtBPostalCode.BorderBrush = defaultColor;
            }
            catch (ValidationException)
            {

                foreach (var item in currentAddress.GetValidationErrors().ToList())
                {
                    if (item.ToString() == "PostalCode")
                    {
                        this.addressAdmin.UCAddress.txtBPostalCode.BorderBrush = Brushes.Red;
                    }
                }
            }
        }
        #endregion
    }
}
