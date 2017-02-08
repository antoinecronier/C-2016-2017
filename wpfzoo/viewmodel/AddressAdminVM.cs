using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.addressAdmin.UCAddressList.LoadItem((await addressManager.Get()).ToList());
        }

        private void InitUC()
        {
            currentAddress = new Address();
            this.addressAdmin.UCAddress.Address = currentAddress;

        }

        private void InitActions()
        {
            this.addressAdmin.btnValidate.Click += BtnValidate_Click;
            this.addressAdmin.UCAddressList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Address item = (e.AddedItems[0] as Address);
                this.addressAdmin.UCAddress.Address = item;
            }
        }

        private void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            addressManager.Insert(this.addressAdmin.UCAddress.Address);
        }
    }
}
