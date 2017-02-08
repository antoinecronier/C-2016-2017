using System;
using System.Collections.Generic;
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
using wpfzoo.viewmodel;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour EmployeeAdmin.xaml
    /// </summary>
    public partial class EmployeeAdmin : Page
    {
        public EmployeeAdmin()
        {
            InitializeComponent();
            this.DataContext = new EmployeeAdminVM(this);
            /*this.ucEmployeeList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            InitLists();*/
        }
        /*
        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Employee item = (e.AddedItems[0] as Employee);
            }
        }

        private async void InitLists()
        {
            MySQLManager<Employee> employeeManager = new MySQLManager<Employee>();
            this.ucEmployeeList.LoadItem((await employeeManager.Get()).ToList());
        }

        private void btnDelEmployee_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        */
    }
}
