using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.entities.enums;
using wpfzoo.views.administration;
using wpfzoo.views.usercontrols;

namespace wpfzoo.viewmodel
{
    public class StreetNumberAdminVM
    {
        private StreetNumber currentStreetNumber;
        private StreetNumberAdmin streetNumberAdmin;
        private MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
        private ListStreetNumberUserControl ucStreetNumberList;

        public StreetNumberAdminVM(StreetNumberAdmin streetNumberAdmin)
        {
            this.streetNumberAdmin = streetNumberAdmin;

            InitUC();
            InitActions();
            InitLists();
        }

        private void InitUC()
        {
            currentStreetNumber = new StreetNumber();
            this.streetNumberAdmin.ucStreetNumber.StreetNumber = currentStreetNumber;
            foreach (StreetAvaibleItems suf in Enum.GetValues(typeof(StreetAvaibleItems)))
            {
                this.streetNumberAdmin.ucStreetNumber.cbSuff.Items.Add(suf);
            }
        }

        private void InitActions()
        {
            this.streetNumberAdmin.New.Click += BtnValidate_Click;
            this.streetNumberAdmin.Delete.Click += BtnDelete_Click;
            this.streetNumberAdmin.ok.Click += BtnOk_Click;
            this.streetNumberAdmin.ucStreetNumberList.itemList.SelectionChanged += ItemsList_SelectionChanged;
            this.streetNumberAdmin.menuDuplicateStreetNumber.Click += DuplicateStreetNumberContextMenu_OnClick;
        }

        private bool TestValue()
        {
            bool test = true;
            var value = this.streetNumberAdmin.ucStreetNumber.txtBNumber.Text;
            Regex rx = new Regex(@"(?:\d)+", RegexOptions.IgnoreCase);

            if (rx.IsMatch(value))
            {
                streetNumberAdmin.ucStreetNumber.txtBNumber.BorderBrush = Brushes.Red;
                MessageBox.Show("Erreur La valeur saisie n'est pas numeric");
                test = false;
            }
            return test;
        }

        private async void DuplicateStreetNumberContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.streetNumberAdmin.ucStreetNumberList.ItemsList.SelectedIndex > -1)
            {
                var streeNumber = new StreetNumber();
                streeNumber = (StreetNumber)this.streetNumberAdmin.ucStreetNumberList.ItemsList.SelectedItem;
                await streetNumberManager.Insert(streeNumber);
                InitLists();
            }
        }

        private async void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TestValue())
            {
                await streetNumberManager.Insert(this.streetNumberAdmin.ucStreetNumber.StreetNumber);
            }

        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            await streetNumberManager.Delete(this.streetNumberAdmin.ucStreetNumber.StreetNumber);
        }

        private async void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (TestValue())
            {
                MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
                await streetNumberManager.Update(this.streetNumberAdmin.ucStreetNumber.StreetNumber);
            }
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                this.streetNumberAdmin.ucStreetNumber.StreetNumber = (e.AddedItems[0] as StreetNumber);
            }
        }

        private async void InitLists()
        {
            this.streetNumberAdmin.ucStreetNumberList.LoadItem((await streetNumberManager.Get()).ToList());
        }
    }
}