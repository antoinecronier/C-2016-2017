using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.entities.enums;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour StreetNumberAdmin.xaml
    /// </summary>
    public partial class StreetNumberAdmin : Page
    {

        ObservableCollection<StreetNumber> streetNumberList = new ObservableCollection<StreetNumber>();


        public StreetNumberAdmin()
        {
            InitializeComponent();
            InitLists();
            this.UCStreetNumberList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                this.UCStreetNumber.StreetNumber = (e.AddedItems[0] as StreetNumber);
            }
            
        }

        private async void InitLists()
        {
            MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
            this.UCStreetNumberList.LoadItem((await streetNumberManager.Get()).ToList());
        }

        //insert Fonctionne
        private async void New_Click(object sender, RoutedEventArgs e)
        {
            MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
            StreetNumber item = new StreetNumber();
            item.Number = Convert.ToInt32(this.UCStreetNumber.txtBNumber.Text);
            item.Suf = (StreetAvaibleItems)Enum.Parse(typeof(StreetAvaibleItems), this.UCStreetNumber.txtBSuf.Text);
            await streetNumberManager.Insert(item);
        }


        private async void ok_Click(object sender, RoutedEventArgs e)
        {
            MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
            await streetNumberManager.Update(UCStreetNumber.StreetNumber);
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            MySQLManager<StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
            await streetNumberManager.Delete(UCStreetNumber.StreetNumber );
        }
    }
}
