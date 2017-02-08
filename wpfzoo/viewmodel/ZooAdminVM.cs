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
            InitActions();
            InitModel();
            InitEvent();
        }

        private void InitEvent()
        {
            this.zooAdmin.UCZooList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        private void InitModel()
        {
            ObservableCollection<Zoo> zooList = new ObservableCollection<Zoo>();
            InitLists();
        }

        private async void InitLists()
        {
            MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
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
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Zoo item = (e.AddedItems[0] as Zoo);
                this.zooAdmin.ucZoo.Zoo = item;
            }
        }

        private void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.zooAdmin.ucZoo.Zoo.Id != 0)
            {
                Task<Zoo> tZoo = zooManager.Insert(this.zooAdmin.ucZoo.Zoo);
                Zoo zoo = (Zoo)tZoo.Result;
            }
            else
            {
                Task<Zoo> tZoo = zooManager.Update(this.zooAdmin.ucZoo.Zoo);
                Zoo zoo = (Zoo)tZoo.Result;
            }
            
        }

        private void btnDelZoo_Click(object sender, RoutedEventArgs e)
        {
            MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
            this.zooAdmin.UCZooList.Obs.Remove(zooAdmin.ucZoo.Zoo);
            Task<Int32> tRes = zooManager.Delete(zooAdmin.ucZoo.Zoo);
            Int32 res = (Int32)tRes.AsyncState;

        }
    }
}

