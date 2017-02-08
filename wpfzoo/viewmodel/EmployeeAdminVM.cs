using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;

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
            this.employeeAdmin.btnValidate.Click += BtnValidate_Click;
        }

        private void BtnValidate_Click(object sender, RoutedEventArgs e)
        {
            employeeManager.Insert(this.employeeAdmin.ucEmployee.Employee);
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
    }
}
