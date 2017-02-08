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
using wpfzoo.viewmodel;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour StreetNumberAdmin.xaml
    /// </summary>
    public partial class StreetNumberAdmin : Page
    {


        //ObservableCollection<StreetNumber> streetNumberList = new ObservableCollection<StreetNumber>();

        public StreetNumberAdmin()
        {
            InitializeComponent();
            InitLists();
            this.DataContext = new StreetNumberAdminVM(this);

        }


    }
}
