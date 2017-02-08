using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;

namespace wpfzoo.viewmodel
{
    public class StreetNumberAdminVM
    {
        private StreetNumber currentStreetNumber;
        private StreetNumberAdmin streetNumberAdmin;
        private MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();

        public StreetNumberAdminVM(StreetNumberAdmin streetNumberAdmin)
        {
            this.streetNumberAdmin= streetNumberAdmin;

            InitUC();
            InitActions();
        }

        private void InitUC()
        {
            currentStreetNumber = new StreetNumber();
            this.streetNumberAdmin.UCStreetNumber.StreetNumber = currentStreetNumber;
        }

        private void InitActions()
        {
            this.streetNumberAdmin.New.Click += BtnValidate_Click;
            this.streetNumberAdmin.Delete.Click += BtnDelete_Click;
            this.streetNumberAdmin.ok.Click += BtnOk_Click;
        }

        private async void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            await streetNumberManager.Update(this.streetNumberAdmin.UCStreetNumber.StreetNumber);
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await streetNumberManager.Delete(this.streetNumberAdmin.UCStreetNumber.StreetNumber);
        }

        private async void BtnValidate_Click(object sender, RoutedEventArgs e)
        {
            await streetNumberManager.Insert(this.streetNumberAdmin.UCStreetNumber.StreetNumber);
        }
    }
}
}
