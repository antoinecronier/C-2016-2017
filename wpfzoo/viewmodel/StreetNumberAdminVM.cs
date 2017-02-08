using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;
using wpfzoo.views.usercontrols;

namespace wpfzoo.viewmodel
{
    public class StreetNumberAdminVM
    {
        private StreetNumber currentStreetNumber;
        private StreetNumberAdmin streetNumberAdmin;
        private MySQLManager<StreetNumber> streetNumberManager= new MySQLManager<StreetNumber>();
        private ListStreetNumberUserControl ucStreetNumberList;

        public StreetNumberAdminVM(StreetNumberAdmin streetNumberAdmin)
        {
            this.streetNumberAdmin = streetNumberAdmin;

            InitUC();
            InitActions();
        }

        private void InitUC()
        {
            currentStreetNumber = new StreetNumber();
            this.streetNumberAdmin.ucStreetNumber.StreetNumber = currentStreetNumber;
        }

        private void InitActions()
        {
            this.streetNumberAdmin.New.Click += BtnValidate_Click;
            this.streetNumberAdmin.Delete.Click += BtnDelete_Click;
            this.streetNumberAdmin.ok.Click += BtnOk_Click;
            this.ucStreetNumberList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                this.ucStreetNumber.StreetNumber = (e.AddedItems[0] as StreetNumber);
                this.ucStreetNumber = item;
            }

        }



        private async void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await streetNumberManager.Insert(this.streetNumberAdmin.ucStreetNumber.StreetNumber);
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await streetNumberManager.Delete(this.streetNumberAdmin.ucStreetNumber.StreetNumber);
        }

        private async void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
            await streetNumberManager.Update(this.streetNumberAdmin.ucStreetNumber.StreetNumber);
        }

        private async void InitLists()
        {
            MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
            this.ucStreetNumberList.LoadItem((await streetNumberManager.Get()).ToList());
        }

        //insert Fonctionne
        private async void New_Click(object sender, RoutedEventArgs e)
        {
            MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
            StreetNumber item = new StreetNumber();
            item.Number = Convert.ToInt32(this.ucStreetNumber.txtBNumber.Text);
            item.Suf = (StreetAvaibleItems)Enum.Parse(typeof(StreetAvaibleItems), this.ucStreetNumber.txtBSuf.Text);
            await streetNumberManager.Insert(item);
        }
    }
}
