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
        private const String RegexName = "^[a-zA-Z]+$-*"; //Work with method checkRegex()
        private readonly string[] ListName = {"Last name", "First name", "Manager last name", "Manager first name" }; //Work with method checkRegexTxtBName()
        List<TextBox> listTxtB = new List<TextBox>();
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

            listTxtB.Clear();
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBLastname);
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBFirstname);
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBManagerFirstname);
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBManagerLastname);
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
                clearTextBBg(listTxtB);
            }
        }

        public bool checkRegexTxtBName()
        {
            bool output = true;

            for (int index = 0; index < ListName.Length; index++)
            {
                if (!checkRegex(listTxtB.ElementAt(index), RegexName))
                {
                    listTxtB.ElementAt(index).Background = Brushes.Red;
                    MessageBox.Show(ListName[index] + " is not valid.");
                    output = false;
                    break;
                }
            }

            return output;
        }

        public void clearTextBBg(List<TextBox> listTextBoxs)
        {
            for (int index = 0; index < ListName.Length; index++)
            {
                listTextBoxs.ElementAt(index).Background = Brushes.White;
            }
        }

        private async void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkRegexTxtBName())
            {
                await employeeManager.Update(this.employeeAdmin.ucEmployee.Employee);
                InitLUC();
                clearTextBBg(listTxtB);
            }
        }

        private async void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkRegexTxtBName())
            {
                await employeeManager.Insert(this.employeeAdmin.ucEmployee.Employee);
                InitLUC();
                clearTextBBg(listTxtB);
            } 
        }
    }
}
