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
using System.Windows.Input;

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

        /**
         * Instantiate new View Model with admin view
         */
        public AddressAdminVM(AddressAdmin addressAdmin)
        {
            this.addressAdmin = addressAdmin;
            InitLists();
            InitUC();
            InitActions();
        }
        #endregion

        #region Init methods

        /**
         * Load entities from database
         */
        private async void InitLists()
        {
            this.addressAdmin.UCAddressList.LoadItems((await addressManager.Get()).ToList());
        }

        /**
         * Instantiate new Address entity with nested StreetNumber with default values
         */
        private void InitUC()
        {
            this.resetAddress();
            defaultColor = this.addressAdmin.UCAddress.txtBPostalCode.BorderBrush;
            foreach (var item in Enum.GetValues(typeof(StreetAvaibleItems)))
            {
                this.addressAdmin.UCAddress.UCStreetNumber.cbSuff.Items.Add(item);
            }
        }

        /**
         * Bind events
         */
        private void InitActions()
        {
            this.addressAdmin.btnValidate.Click += BtnValidate_Click;
            this.addressAdmin.btnNew.Click += BtnNew_Click;
            this.addressAdmin.btnDelete.Click += BtnDelete_Click;
            this.addressAdmin.btnVerify.Click += BtnVerify_Click;
            this.addressAdmin.UCAddressList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            this.addressAdmin.UCAddressList.RemoveAddressContextMenu.Click += RemoveAddressContextMenu_OnClick;
            this.addressAdmin.UCAddressList.DuplicateAddressContextMenu.Click += DuplicateAddressContextMenu_OnClick;
        }
        #endregion

        #region events methods
        #region List

        /**
         * Callback on selected item in list
         */
        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (hasFieldsChanged())
                {
                    MessageBoxResult mbr = MessageBox.Show("You have some unsaved data. Do you want to wipe them all ? (cannot be undone)", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

                    if (mbr == MessageBoxResult.OK)
                    {
                        Address item = (e.AddedItems[0] as Address);
                        currentAddress = item;
                        addressManager.GetStreetNumber(currentAddress);
                        this.addressAdmin.UCAddress.Address = currentAddress;
                        this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = currentAddress.StreetNumber;
                        this.reloadList();
                        this.resetFieldsColors();
                    }
                }
                else
                {
                    Address item = (e.AddedItems[0] as Address);
                    currentAddress = item;
                    addressManager.GetStreetNumber(currentAddress);
                    this.addressAdmin.UCAddress.Address = currentAddress;
                    this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = currentAddress.StreetNumber;
                    this.resetFieldsColors();
                }
                
            }
        }
        #endregion

        #region buttons
        #region save

        /**
         * Callback on "Save" button
         */
        private async void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentAddress = this.addressAdmin.UCAddress.Address;

            if (currentAddress.Id != 0)
            {
                try
                {
                    await addressManager.Update(currentAddress);
                }
                catch (DbEntityValidationException dbe)
                {
                    validate();
                    MessageBox.Show("One or more fields are not valid.");
                    Console.WriteLine(dbe);
                    
                }              
            }
            else
            {
                try
                {
                    MySQLManager<StreetNumber> snManager = new MySQLManager<StreetNumber>();
                    await snManager.Insert(currentAddress.StreetNumber);
                    await addressManager.Insert(currentAddress);
                    this.addressAdmin.UCAddressList.AddItem(currentAddress);
                }
                catch (DbEntityValidationException dbe)
                {
                    validate();
                    MessageBox.Show("One or more fields are not valid.");
                    Console.WriteLine(dbe);
                }
            }
        }
        #endregion

        #region new

        /**
         * Callback on "New" button
         */
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            currentAddress = this.addressAdmin.UCAddress.Address;

            if (hasFieldsChanged())
            {
                MessageBoxResult mbr = MessageBox.Show("You have some unsaved data. Do you want to wipe them all ? (cannot be undone)", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

                if (mbr == MessageBoxResult.OK)
                {
                    if (currentAddress.Id != 0)
                    {
                        this.reloadList();
                    }
                    this.resetAddress();
                }
            }
            else
            {
                this.resetAddress();
            }

        }
        #endregion

        #region delete

        /**
         * Callback on "Delete" button
         */
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

        #region verify

        /**
         * Callback on "Verify" button
         */
        private void BtnVerify_Click(object sender, RoutedEventArgs e)
        {
            if (!validate())
            {
                MessageBox.Show("There is one ore more error(s) in your fields. Please check them before saving");
            }
            else
            {
                MessageBox.Show("So far, so good !");
            }
        }
        #endregion
        #endregion

        #region context menu

        /**
         * Callback on "Remove" item in Context menu
         */
        private void RemoveAddressContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            confirmDelete();
        }

        /// <summary>
        /// Callback on "Duplicate" item in Context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DuplicateAddressContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.addressAdmin.UCAddressList.ItemsList.SelectedIndex > -1)
            {   
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

        /**
         * Show a MessageBox for deleting confirmation
         */
        private async void confirmDelete()
        {
            MessageBoxResult mbr = MessageBox.Show("Do you really want to delete this item ?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

            if (mbr == MessageBoxResult.OK)
            {
                await addressManager.Delete(currentAddress);
                this.addressAdmin.UCAddressList.RemoveItem(currentAddress);
                this.resetAddress();
            }
        }

        /**
         * Instantiate new Address entity with nested StreetNumber with default values
         * then reset the current address value
         */
        private void resetAddress()
        {
            currentAddress = new Address(new StreetNumber());
            this.addressAdmin.UCAddress.Address = currentAddress;
            this.addressAdmin.UCAddress.UCStreetNumber.StreetNumber = currentAddress.StreetNumber;
        }

        /**
         * Check fields and border them in red
         * 
         * Return false if any error occured 
         */
        private Boolean validate()
        {
            currentAddress = this.addressAdmin.UCAddress.Address;

            Boolean hasNoErrors = true;

            try
            {
                this.resetFieldsColors();

                EntityValidator.Validate<Address>(currentAddress);

            }
            catch (ValidationException)
            {
                hasNoErrors = false;

                String errorMessages = currentAddress.GetValidationErrorMessages();

                if (errorMessages.Contains("PostalCode"))
                    this.addressAdmin.UCAddress.txtBPostalCode.BorderBrush = Brushes.Red;
                if (errorMessages.Contains("City"))
                    this.addressAdmin.UCAddress.txtBCity.BorderBrush = Brushes.Red;
                if (errorMessages.Contains("Street"))
                    this.addressAdmin.UCAddress.txtBStreet.BorderBrush = Brushes.Red;
            }

            return hasNoErrors;
        }

        /**
         * Check if a field has changed between Edit State (in DB or not) and Save State (in DB)
         */
        private Boolean hasFieldsChanged()
        {
            Boolean result = false;

            Reflectionner reflec = new Reflectionner();
            var dico = reflec.ReadObject<Address>(currentAddress);

            if (dico["Id"].Equals(0)) //Entity is not in database
            {
                dico.Remove("Id");

                foreach (var item in dico)
                {
                    if (item.Key != "StreetNumber" && item.Value != null)
                    {
                        result = true;
                        break;
                    }

                }
            }
            else //Fields not empty, but entity loaded from db
            {
                if (addressManager.ChangeTracker.HasChanges())
                {
                    result = true;
                }
            }

            return result;
        }

        /**
         * Force reloading list items from database
         */
        private void reloadList()
        {
            foreach (var entity in addressManager.ChangeTracker.Entries())
            {
                entity.Reload();
            }

            InitLists();

        }

        /**
         * Reset border color of fields with default one
         */
        private void resetFieldsColors()
        {
            this.addressAdmin.UCAddress.txtBPostalCode.BorderBrush = defaultColor;
            this.addressAdmin.UCAddress.txtBCity.BorderBrush = defaultColor;
            this.addressAdmin.UCAddress.txtBStreet.BorderBrush = defaultColor;
        }
        #endregion
    }
}
