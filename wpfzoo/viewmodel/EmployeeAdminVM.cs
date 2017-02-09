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
using wpfzoo.database.entitieslinks;
using wpfzoo.views.usercontrols;

namespace wpfzoo.viewmodel
{
    public class EmployeeAdminVM
    {
        private const String RegexName = "^[a-zA-Z]+$-*"; //Work with method checkRegex()
        private readonly string[] ListName = {"Last name", "First name", "Manager last name", "Manager first name" }; //Work with method checkRegexTxtBName()
        List<TextBox> listTxtB = new List<TextBox>();
        private Employee currentEmployee;
        private EmployeeAdmin employeeAdmin;
        private MySQLEmployeeManager employeeManager = new MySQLEmployeeManager();
        private AddressAdmin addressAdmin;

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
                currentEmployee = (e.AddedItems[0] as Employee);
                MySQLEmployeeManager mySqlEmployeeManager = new MySQLEmployeeManager();
                mySqlEmployeeManager.GetAddress(currentEmployee);
                this.employeeAdmin.ucEmployee.Employee = currentEmployee;
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
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBManagerLastname);
            listTxtB.Add(this.employeeAdmin.ucEmployee.txtBManagerFirstname);
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
            this.employeeAdmin.ucEmployee.btnAddress.Click += BtnAddress_Click;
            this.employeeAdmin.ucEmployee.btnJobs.Click += BtnJobs_Click;
            this.employeeAdmin.btnAddEmployee.Click += btnAddEmployee_Click;
            this.employeeAdmin.btnUpdateEmployee.Click += btnUpdateEmployee_Click;
            this.employeeAdmin.btnDelEmployee.Click += btnDelEmployee_Click;
            this.employeeAdmin.menuDuplicate.Click += MenuDuplicate_OnClick;
            this.employeeAdmin.menuDelete.Click += MenuDelete_OnClick;
            this.employeeAdmin.ucEmployeeList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            this.employeeAdmin.ucEmployee.DatePHiring.KeyDown += disableTypingDatePHiring;
            this.employeeAdmin.ucEmployee.DatePBirth.KeyDown += disableTypingDatePBirth;
        }

        private void BtnJobs_Click(object sender, RoutedEventArgs e)
        {
            this.employeeAdmin.NavigationService.Navigate(new JobAdmin(this));
        }

        public void LoadAddressPage(AddressAdmin addressAdmin)
        {
            this.addressAdmin = addressAdmin;
        }

        private void BtnAddress_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee.Address != null)
            {
                this.employeeAdmin.NavigationService.Navigate(new AddressAdmin(this));

                /*AddressAdmin addressAdmin = new AddressAdmin();
                Window window = new Window();
                window.Content = addressAdmin;
                window.Show();
                addressAdmin.UCAddress.Address = currentEmployee.Address;*/
            }
            else
            {
                MessageBox.Show("Can't open because address is null");
            }

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

        public bool checkDateP()
        {
            DatePicker datePHiring = this.employeeAdmin.ucEmployee.DatePHiring;
            DatePicker datePBirth = this.employeeAdmin.ucEmployee.DatePBirth;

            datePHiring.Background = Brushes.White;
            datePBirth.Background = Brushes.White;

            int hiring = int.Parse(datePHiring.DisplayDate.ToString("yyyyMMdd"));
            int birth = int.Parse(datePBirth.DisplayDate.ToString("yyyyMMdd"));
            int age = (hiring - birth) / 10000;

            if (datePHiring.DisplayDate < datePBirth.DisplayDate && age >= 18)
            {
                return true;
            }
            else
            {
                datePHiring.Background = Brushes.Red;
                datePBirth.Background = Brushes.Red;
                MessageBox.Show("Birth > Hiring or (Hiring - Birth) < 18");
                return false;
            }
            
        }

        public bool checkRegexTxtBName()
        {
            bool output = true;

            clearTextBBg(listTxtB);

            for (int index = 0; index < ListName.Length; index++)
            {
                if (!checkRegex(listTxtB.ElementAt(index), RegexName))
                {
                    if (index > 1 && !listTxtB.ElementAt(index).Text.Equals(""))
                    {
                        listTxtB.ElementAt(index).Background = Brushes.Red;
                        MessageBox.Show(ListName[index] + " is not valid.");
                        output = false;
                    }
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
            if (checkRegexTxtBName() && checkDateP())
            {
                await employeeManager.Update(this.employeeAdmin.ucEmployee.Employee);
                InitLUC();
            }
        }

        private async void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkRegexTxtBName() && checkDateP())
            {
                await employeeManager.Insert(this.employeeAdmin.ucEmployee.Employee);
                InitLUC();
            } 
        }

        #region JobAdmin

        private Job currentJob;
        private JobAdmin jobAdmin;
        private MySQLManager<Job> jobManager = new MySQLManager<Job>();
        ListJobUserControl newListControl;
        List<Job> allJobsInsert = new List<Job>();

        public void LoadJobAdmin(JobAdmin jobAdmin)
        {
            this.jobAdmin = jobAdmin;
            JobAdminAddUI();
            InitUCJobAdmin();
            InitLUCJobAdmin();
            InitActionsJobAdmin();
        }

        private void JobAdminAddUI()
        {
            this.jobAdmin.mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            int oldColumn = Grid.GetColumn(this.jobAdmin.UCJobList);
            newListControl = new ListJobUserControl();
            newListControl.Name = "UCJobListDB";
            Grid.SetColumn(newListControl, oldColumn + 1);
            int oldRowSpan = Grid.GetRowSpan(this.jobAdmin.UCJobList);
            Grid.SetRowSpan(newListControl, oldRowSpan);
            this.jobAdmin.mainGrid.Children.Add(newListControl);

            this.jobAdmin.btnDelJob.Content = "Back";
            this.jobAdmin.btnUpdateJob.Content = "Validate";

            newListControl.itemList.ContextMenu.Items.Remove(newListControl.RemoveJobContextMenu);
            this.jobAdmin.UCJobList.itemList.ContextMenu.Items.Remove(this.jobAdmin.UCJobList.RemoveJobContextMenu);

            newListControl.DuplicateJobContextMenu.Header = "Remove Item";
            newListControl.DuplicateJobContextMenu.Click += DuplicateJobContextMenu_Click;
            this.jobAdmin.UCJobList.DuplicateJobContextMenu.Header = "Add Item";
            this.jobAdmin.UCJobList.DuplicateJobContextMenu.Click += DuplicateJobContextMenu_Click;
        }

        private void DuplicateJobContextMenu_Click(object sender, RoutedEventArgs e)
        {
            if (allJobsInsert.Contains(this.currentJob))
            {
                this.jobAdmin.UCJobList.Obs.Remove(this.currentJob);
                this.newListControl.Obs.Add(this.currentJob);
            }
            else if (this.currentEmployee.Jobs.Contains(currentJob))
            {
                this.jobAdmin.UCJobList.Obs.Add(this.currentJob);
                this.newListControl.Obs.Remove(this.currentJob);
            }
        }

        private void InitUCJobAdmin()
        {
            currentJob = new Job();
            this.jobAdmin.UCJob.Job = currentJob;
        }

        private async void InitLUCJobAdmin()
        {
            List<Job> allJobs = (await jobManager.Get()).ToList();
            employeeManager.GetJobs(this.currentEmployee);
            Boolean flag = true;
            
            foreach (var item in allJobs)
            {
                foreach (var item1 in this.currentEmployee.Jobs)
                {
                    if (item.Id == item1.Id)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    allJobsInsert.Add(item);
                }
            }

            newListControl.LoadItem(this.currentEmployee.Jobs);
            this.jobAdmin.UCJobList.LoadItem(allJobsInsert);
        }

        private void InitActionsJobAdmin()
        {
            this.jobAdmin.btnUpdateJob.Click += btnUpdateJob_Click;
            this.jobAdmin.btnDelJob.Click += btnDelJob_Click;
            this.jobAdmin.UCJobList.itemList.SelectionChanged += ItemList_SelectionChanged;
            newListControl.itemList.SelectionChanged += ItemList_SelectionChanged;
        }

        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                currentJob = (e.AddedItems[0] as Job);
                this.jobAdmin.UCJob.Job = currentJob;
            }
        }

        private async void btnDelJob_Click(object sender, RoutedEventArgs e)
        {
            //Back

        }

        private async void btnUpdateJob_Click(object sender, RoutedEventArgs e)
        {
            //Validate

        }

        private Boolean checkValidity(Job job)
        {
            var regexName = new Regex(@"^[A-Z][-a-zA-Z]+$");
            var regexSalary = new Regex(@"[0-9]+(\.[0-9][0-9]?)?");

            if (regexName.Match(job.Name).Success && regexSalary.Match(job.Salary.ToString()).Success)
            {
                return true;
            }
            else
            {
                System.Windows.MessageBox.Show("Please check fields");
                return false;
            }
        }

        #endregion
    }
}
