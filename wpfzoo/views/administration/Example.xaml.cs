using ClassLibrary2.Entities.Generator;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.json;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour Example.xaml
    /// </summary>
    public partial class Example : Page
    {
        ObservableCollection<Address> addressList = new ObservableCollection<Address>();
        ObservableCollection<StreetNumber> streetNumberList = new ObservableCollection<StreetNumber>();
        public Example()
        {
            InitializeComponent();
            this.UCAddressList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            InitLists();
            //AsyncExample();
        }

        //private void AsyncExample()
        //{
        //    MySQLManager<Address> addMan = new MyS
        //    Task.Factory.StartNew(() =>
        //    {
        //        EntityGenerator<Address> generator = new EntityGenerator<Address>();
        //        while (true)
        //        {
        //            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new D);
        //            this.UCAddressList.Obs.Add(generator.GenerateItem());
        //        }
        //    });
        //}

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Address item = (e.AddedItems[0] as Address);

            }
        }

        private async void InitLists()
        {
            MySQLManager<Address> addressManager = new MySQLManager<Address>();
            this.UCAddressList.LoadItem((await addressManager.Get()).ToList());

            MySQLManager <StreetNumber> streetNumberManager = new MySQLManager<StreetNumber>();
            this.UCStreetNumberList.LoadItem((await streetNumberManager.Get()).ToList());
        }

        private void UCAddressList_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
