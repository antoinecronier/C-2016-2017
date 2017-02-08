using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;

namespace wpfzoo.viewmodel
{
    class AddressAdminVM
    {
        private Address currentAddress;
        private AddressAdmin addressAdmin;
        private MySQLManager<Address> addressManager = new MySQLManager<Address>();

        public AddressAdminVM(AddressAdmin addressAdmin)
        {
            this.addressAdmin = addressAdmin;
            InitLists();
            InitUC();
            InitActions();
        }

        private async void InitLists()
        {
            this.addressAdmin.UCAddressList.LoadItems((await addressManager.Get()).ToList());
        }

        private void InitUC()
        {
            this.ResetAddress();

        }

        private void InitActions()
        {
            this.addressAdmin.btnValidate.Click += BtnValidate_Click;
            this.addressAdmin.btnNew.Click += BtnNew_Click;
            this.addressAdmin.btnDelete.Click += BtnDelete_Click;
            this.addressAdmin.UCAddressList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Address item = (e.AddedItems[0] as Address);
                currentAddress = item;
                this.addressAdmin.UCAddress.Address = currentAddress;
            }
        }

        private async void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentAddress = this.addressAdmin.UCAddress.Address;

            if (currentAddress.Id != 0)
            {
                await addressManager.Update(currentAddress);
            }
            else
            {
                await addressManager.Insert(currentAddress);
                this.addressAdmin.UCAddressList.AddItem(currentAddress);
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

            currentAddress = this.addressAdmin.UCAddress.Address;

            // Check if we have filled props
            if (currentAddress.GetType().GetProperties().Any(value => value != null ))
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

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            currentAddress = this.addressAdmin.UCAddress.Address;

            if (currentAddress.Id == 0)
            {
                MessageBox.Show("Cannot delete new element in database");
            }
            else
            {
                 MessageBoxResult mbr = MessageBox.Show("Do you really want to delete this item ?","Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

                if (mbr == MessageBoxResult.OK)
                {
                    await addressManager.Delete(currentAddress);
                    this.addressAdmin.UCAddressList.RemoveItem(currentAddress);
                    this.ResetAddress();
                }          
            }
        }

        private void ResetAddress()
        {
            currentAddress = new Address();
            this.addressAdmin.UCAddress.Address = currentAddress;
        }
    }
}
