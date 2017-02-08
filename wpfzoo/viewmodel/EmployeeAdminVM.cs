using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;
using System.Windows.Controls;

namespace wpfzoo.viewmodel
{
    public class EmployeeAdminVM
    {
        private Employee currentEmployee;
        private EmployeeAdmin employeeAdmin;
        private MySQLManager<Employee> employeeManager = new MySQLManager<Employee>();

        public EmployeeAdminVM(EmployeeAdmin employeeAdmin)
        {
            this.employeeAdmin = employeeAdmin;
            InitUC();
            InitLUC();
            InitActions();
        }


        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Employee item = (e.AddedItems[0] as Employee);
                this.employeeAdmin.ucEmployee.Employee = item;
            }
        }

        private void InitUC()
        {
            currentEmployee = new Employee();
            this.employeeAdmin.ucEmployee.Employee = currentEmployee;
        }

        private async void InitLUC()
        {
            this.employeeAdmin.ucEmployeeList.LoadItem((await employeeManager.Get()).ToList());
        }

        private void InitActions()
        {
            this.employeeAdmin.btnAddEmployee.Click += btnAddEmployee_Click;
            this.employeeAdmin.btnUpdateEmployee.Click += btnUpdateEmployee_Click;
            this.employeeAdmin.btnDelEmployee.Click += btnDelEmployee_Click;
            this.employeeAdmin.ucEmployeeList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
        }

        private async void btnDelEmployee_Click(object sender, RoutedEventArgs e)
        {
            await employeeManager.Delete(this.employeeAdmin.ucEmployee.Employee);
        }

        private async void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            await employeeManager.Update(this.employeeAdmin.ucEmployee.Employee);
        }

        private async void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            await employeeManager.Insert(this.employeeAdmin.ucEmployee.Employee);
        }
    }
}
