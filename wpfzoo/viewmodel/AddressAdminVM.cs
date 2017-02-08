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
    class AddressAdminVM
    {
        private Address currentAddress;
        private AddressAdmin addressAdmin;
        private MySQLManager<Address> addressManager = new MySQLManager<Address>();

        public AddressAdminVM(AddressAdmin addressAdmin)
        {
            this.addressAdmin = addressAdmin;
            InitUC();
            InitActions();
        }

        private void InitUC()
        {
            currentAddress = new Address();
            this.addressAdmin.ucEmployee.Address = currentAddress;
        }

        private void InitActions()
        {
            this.addressAdmin.btnValidate.Click += BtnValidate_Click;
        }

        private void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            addressManager.Insert(this.addressAdmin.ucEmployee.Address);
        }
    }
}
