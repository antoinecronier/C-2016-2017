using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ZooAdminVM
    {
        private Zoo currentZoo;
        private ZooAdmin zooAdmin;
        private MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();

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
            this.zooAdmin.ucZoo.Zoo = currentZoo;
        }

        private void InitActions()
        {
            this.zooAdmin.btnValidateZoo.Click += BtnValidate_Click;
            this.zooAdmin.btnDelZoo.Click += BtnDel_Click;
            this.zooAdmin.btnNewZoo.Click += BtnNew_Click;
            this.zooAdmin.UCZooList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Zoo item = (e.AddedItems[0] as Zoo);
                this.zooAdmin.ucZoo.Zoo = item;
            }
        }

        private async void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.zooAdmin.ucZoo.Zoo.Id != 0)
            {
                await zooManager.Update(this.zooAdmin.ucZoo.Zoo);
            }
            else
            {
                Task<Zoo> tZoo = zooManager.Insert(this.zooAdmin.ucZoo.Zoo);
                Zoo zoo = (Zoo)tZoo.Result;
                this.zooAdmin.ucZoo.Zoo = zoo;
                InitLUC();
            }

        }

        private async void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            this.zooAdmin.UCZooList.Obs.Remove(zooAdmin.ucZoo.Zoo);
            await zooManager.Delete(zooAdmin.ucZoo.Zoo);
            currentZoo = new Zoo();
            this.zooAdmin.ucZoo.Zoo = currentZoo;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            InitUC();
        }
    }
}

