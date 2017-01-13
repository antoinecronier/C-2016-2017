using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.usercontrols;
using ListView = System.Windows.Forms.ListView;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Interaction logic for EmployeeAdmin.xaml
    /// </summary>
    public partial class EmployeeAdmin : Page
    {
        ObservableCollection<Address> emloyeeList = new ObservableCollection<Address>();

        public EmployeeAdmin()
        {
            InitializeComponent();
            InitLists();
        }

        private async void InitLists()
        {
            MySQLManager<Employee> addressManager = new MySQLManager<Employee>();
            this.LUCListEmployee.LoadItem((await addressManager.Get()).ToList());


        }
    }
}
