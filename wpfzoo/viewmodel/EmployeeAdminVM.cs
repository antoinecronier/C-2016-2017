using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using wpfzoo.database;
using wpfzoo.entities;
using wpfzoo.views.administration;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using wpfzoo.entities.enums;
using System.Windows.Media;

namespace wpfzoo.viewmodel
{
    public class EmployeeAdminVM
    {
        private const String REGEXNAME = "^[a-zA-Z]+$-*";
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
            currentEmployee.Birth = DateTime.Now;
            currentEmployee.Hiring = DateTime.Now;
            this.employeeAdmin.ucEmployee.Employee = currentEmployee;

            foreach (Gender gender in Enum.GetValues(typeof(Gender)))
            {
                this.employeeAdmin.ucEmployee.cboCGender.Items.Add(gender);
            }
        }

        public void disableTypingDatePHiring(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        public void disableTypingDatePBirth(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private bool checkRegex(TextBox valueTested, String regex)
        {
            Match match = Regex.Match(valueTested.Text, regex);

            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            this.employeeAdmin.menuDuplicate.Click += MenuDuplicate_OnClick;
            this.employeeAdmin.menuDelete.Click += MenuDelete_OnClick;
            this.employeeAdmin.ucEmployeeList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            this.employeeAdmin.ucEmployee.datePHiring.KeyDown += disableTypingDatePHiring;
            this.employeeAdmin.ucEmployee.DatePBirth.KeyDown += disableTypingDatePBirth;
        }

        private async void MenuDuplicate_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.employeeAdmin.ucEmployeeList.itemList.SelectedItems.Count > 0)
            {
                Employee dupplicateEmployee = new Employee(this.employeeAdmin.ucEmployeeList.itemList.SelectedItem as Employee);
                await employeeManager.Insert(dupplicateEmployee);
                InitLUC();
            }
        }

        private async void MenuDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.employeeAdmin.ucEmployeeList.itemList.SelectedItems.Count > 0)
            {
                Employee deleteEmployee = this.employeeAdmin.ucEmployeeList.itemList.SelectedItem as Employee;
                await employeeManager.Delete(deleteEmployee);
                InitLUC();
            }
        }

        private async void btnDelEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkRegexTxtBName())
            {
                await employeeManager.Delete(this.employeeAdmin.ucEmployee.Employee);
                InitLUC();
                InitUC();
            }
        }

        public bool checkRegexTxtBName()
        {
            TextBox txtBLastName = this.employeeAdmin.ucEmployee.txtBLastname;
            TextBox txtBFirstname = this.employeeAdmin.ucEmployee.txtBFirstname;
            TextBox txtBManagerFirstname = this.employeeAdmin.ucEmployee.txtBManagerFirstname;
            TextBox txtBManagerLastname = this.employeeAdmin.ucEmployee.txtBManagerLastname;

            if (checkRegex(txtBLastName, REGEXNAME))
            {
                if (checkRegex(txtBFirstname, REGEXNAME))
                {
                    if (checkRegex(txtBManagerLastname, REGEXNAME))
                    {
                        if (checkRegex(txtBManagerFirstname, REGEXNAME))
                        {
                            return true;
                        }
                        else
                        {
                            this.employeeAdmin.ucEmployee.txtBManagerFirstname.Background = Brushes.Red;
                            MessageBox.Show("First name of manager is not valid.");
                            return false;
                        }
                    }
                    else
                    {
                        this.employeeAdmin.ucEmployee.txtBManagerLastname.Background = Brushes.Red;
                        MessageBox.Show("Last name of manager is not valid.");
                        return false;
                    }
                }
                else
                {
                    this.employeeAdmin.ucEmployee.txtBFirstname.Background = Brushes.Red;
                    MessageBox.Show("First name is not valid.");
                    return false;
                }
            }
            else
            {
                this.employeeAdmin.ucEmployee.txtBLastname.Background = Brushes.Red;
                MessageBox.Show("Last name is not valid.");
                return false;
            }
        }

        private async void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkRegexTxtBName())
            {
                await employeeManager.Update(this.employeeAdmin.ucEmployee.Employee);
                InitLUC();
            }
        }

        private async void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkRegexTxtBName())
            {
                await employeeManager.Insert(this.employeeAdmin.ucEmployee.Employee);
                InitLUC();
            } 
        }
    }
}
