using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using wpfzoo.database;
using wpfzoo.database.entitieslinks;
using wpfzoo.entities;
using wpfzoo.views.administration;
using wpfzoo.entities.enums;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Forms;

namespace wpfzoo.viewmodel
{
    public class ZooAdminVM
    {
        private Zoo currentZoo;
        private ZooAdmin zooAdmin;
        private MySQLManager<Zoo> zooManager = new MySQLManager<Zoo>();
        private MySQLZooManager zooLinkManager = new MySQLZooManager();
        private StructureAdmin structureAdmin;
        private ScheduleAdmin scheduleAdmin;
        private AnimalAdmin animalAdmin;
        private EmployeeAdmin employeeAdmin;
        private const String RegexName = "^[a-zA-Z]+$-*"; //Work with method checkRegex()
        private readonly string[] ListName = { "Last name", "First name", "Manager last name", "Manager first name" }; //Work with method checkRegexTxtBName()
        List<System.Windows.Controls.TextBox> listTxtB = new List<System.Windows.Controls.TextBox>();
        private Employee currentEmployee;
        private MySQLManager<Employee> employeeManager = new MySQLManager<Employee>();
        private AddressAdmin addressAdmin;

        #region GestionZoo
        public object UCZooList { get; private set; }

        public ZooAdminVM(ZooAdmin zooAdmin)
        {
            this.zooAdmin = zooAdmin;

            InitUC();
            InitLUC();
            InitActions();
        }

        private async void InitLUC()
        {
            this.zooAdmin.UCZooList.LoadItem((await zooManager.Get()).ToList());
        }

        private void InitUC()
        {
            currentZoo = new Zoo();
            currentZoo.Birth = DateTime.Now;
            this.zooAdmin.ucZoo.Zoo = currentZoo;
        }

        private void InitActions()
        {
            this.zooAdmin.btnValidateZoo.Click += BtnValidate_Click;
            this.zooAdmin.btnDelZoo.Click += BtnDel_Click;
            this.zooAdmin.btnNewZoo.Click += BtnNew_Click;
            this.zooAdmin.UCZooList.DuplicateZooContextMenu.Click += DuplicateZoo_Click;
            this.zooAdmin.UCZooList.RemoveZooContextMenu.Click += BtnDel_Click;
            this.zooAdmin.UCZooList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            this.zooAdmin.btnStructure.Click += BtnStructure_Click;
            this.zooAdmin.btnAddress.Click += BtnAddress_Click;
            this.zooAdmin.btnEmployee.Click += BtnEmployees_Click;
        }

        #region GestionZooBtn
        private void BtnAddress_Click(object sender, RoutedEventArgs e)
        {
            if (currentZoo.Address != null)
            {
                AddressAdmin addressAdmin = new AddressAdmin();
                Window window = new Window();
                window.Content = addressAdmin;
                window.Show();
                addressAdmin.UCAddress.Address = currentZoo.Address;
            }
            else
            {
                AddressAdmin addressAdmin = new AddressAdmin();
                Window window = new Window();
                window.Content = addressAdmin;
                window.Show();
            }

        }

    private async void BtnValidate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.zooAdmin.ucZoo.Zoo.Birth < DateTime.Now)
            {
                if (this.zooAdmin.ucZoo.Zoo.Id != 0)
                {
                    await zooManager.Update(this.zooAdmin.ucZoo.Zoo);
                }
                else
                {
                    Task<Zoo> tZoo = zooManager.Insert(this.zooAdmin.ucZoo.Zoo);
                    Zoo zoo = (Zoo)tZoo.Result;
                    this.zooAdmin.ucZoo.Zoo = zoo;
                    InitLUC();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please do not create a zoo in the future... ", "Delete Zoo",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                currentZoo.Birth = DateTime.Now;
            }

        }

        private async void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (this.zooAdmin.ucZoo.Zoo.Id != 0)
            {
                if (System.Windows.Forms.MessageBox.Show("Do you want to delete " + this.zooAdmin.ucZoo.Zoo.Name + " Zoo?", "Delete Zoo",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                 == DialogResult.Yes)
                {
                    this.zooAdmin.UCZooList.Obs.Remove(zooAdmin.ucZoo.Zoo);
                    await zooManager.Delete(zooAdmin.ucZoo.Zoo);
                    currentZoo = new Zoo();
                    this.zooAdmin.ucZoo.Zoo = currentZoo;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("You must select an object to delete it... ", "Delete Zoo",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            InitUC();
        }

        private void DuplicateZoo_Click(object sender, RoutedEventArgs e)
        {
            Zoo zoo = new entities.Zoo();
            zoo.Birth = this.zooAdmin.ucZoo.Zoo.Birth;
            zoo.Address = this.zooAdmin.ucZoo.Zoo.Address;
            zoo.Name = this.zooAdmin.ucZoo.Zoo.Name;
            zoo.Staff = this.zooAdmin.ucZoo.Zoo.Staff;
            zoo.Structures = this.zooAdmin.ucZoo.Zoo.Structures;

            Task<Zoo> tZoo = zooManager.Insert(zoo);
            Zoo zooRes = (Zoo)tZoo.Result;
            this.zooAdmin.ucZoo.Zoo = zooRes;
            InitLUC();
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Zoo item = (e.AddedItems[0] as Zoo);
                this.zooAdmin.ucZoo.Zoo = item;
            }
        }

        #endregion

        #endregion

        #region GestionStructure

        private void BtnStructure_Click(object sender, RoutedEventArgs e)
        {
            if (currentZoo.Structures != null)
            {
                this.zooAdmin.NavigationService.Navigate(new StructureAdmin(this));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Can't open because structure is null", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region GestionLoadStructure

        public void LoadStructurePage(StructureAdmin structureAdmin)
        {
            this.structureAdmin = structureAdmin;
            InitLUCStructure();
            InitUC();
            ClicksGenerator();
        }

        public void LoadSchedulePage(ScheduleAdmin scheduleAdmin)
        {
            this.scheduleAdmin = scheduleAdmin;
            InitLUCSchedule();
            InitUC();
            ClicksGenerator();
        }

        public void LoadEmployeePage(ScheduleAdmin scheduleAdmin)
        {
            this.scheduleAdmin = scheduleAdmin;
            InitLUCEmployee();
            InitUC();
            ClicksGenerator();
        }

        public void LoadAnimalPage(ScheduleAdmin scheduleAdmin)
        {
            this.scheduleAdmin = scheduleAdmin;
            InitLUCAnimal();
            InitUC();
            ClicksGenerator();
        }

        #endregion

        #region GestionStructureInitialisation

        private void InitLUCStructure()
        {
            zooLinkManager.GetStructures(currentZoo);
        }

        #region GestionStructureInitialisationSousPage
        private void InitLUCSchedule()
        {

        }

        private void InitLUCAnimal()
        {
            throw new NotImplementedException();
        } 
        #endregion



        #endregion

        #region GestionStructureEvenement
        private void ClicksGenerator()
        {
            this.structureAdmin.buttonNew.Click += BtnValidateStructure_Click;
            this.structureAdmin.Ok.Click += BtnUpdateStructure_Click;
            this.structureAdmin.Delete.Click += BtnDeleteStructure_Click;
            this.structureAdmin.ucStructure.buttonEmploye.Click += ButtonEmploye_Click;
            this.structureAdmin.ucStructure.buttonAnimaux.Click += ButtonAnimaux_Click;
            this.structureAdmin.ucStructure.buttonSchedule.Click += ButtonSchedule_Click;
        } 
        #endregion

        #region GestionStructure Btn

        private async void BtnDeleteStructure_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private async void BtnUpdateStructure_Click(object sender, RoutedEventArgs e)
        {
            this.structureAdmin.ucStructure.txtBSurface.Text.Contains("-");
            {
                await zooManager.Update(this.zooAdmin.ucZoo.Zoo);
                InitLUCStructure();
            }
        }
        private async void BtnValidateStructure_Click(object sender, RoutedEventArgs e)
        {
            if (!this.structureAdmin.ucStructure.txtBSurface.Text.Contains("-"))
            {
                await zooManager.Insert(this.zooAdmin.ucZoo.Zoo);
                this.structureAdmin.ucStructure.txtBSurface.Text = "";
                this.structureAdmin.ucStructure.txtBName.Text = "";
            }
            else
            {
                System.Windows.MessageBox.Show("Erreur surface négative wsh");
            }
        }

        private void BtnEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (currentZoo.Staff.Count > 0)
            {
                InitUCEmployee();
                InitLUCEmployee();
                InitActionsEmployee();

                EmployeeAdmin employeeAdmin = new EmployeeAdmin();
                Window window = new Window();
                window.Content = employeeAdmin;
                window.Show();
                employeeAdmin.ucEmployeeList.LoadItem(currentZoo.Staff);
            }
            else
            {

                InitUCEmployee();
                InitLUCEmployee();
                InitActionsEmployee();

                EmployeeAdmin employeeAdmin = new EmployeeAdmin();
                Window window = new Window();
                window.Content = employeeAdmin;
                window.Show();
            }
        }

        private async void ButtonAnimaux_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private async void ButtonEmploye_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void ButtonSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (currentZoo.Structures != null)
            {
                this.zooAdmin.NavigationService.Navigate(new ScheduleAdmin(this));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Can't open because structure is null", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStructureSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (currentZoo.Structures!= null)
            {
                this.zooAdmin.NavigationService.Navigate(new ScheduleAdmin(this));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Can't open because schedule is null", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        #endregion

        #region EmployeeAdmin
        private void ItemsList_SelectionChangedEmployee(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                currentEmployee = (e.AddedItems[0] as Employee);
                this.employeeAdmin.ucEmployee.Employee = currentEmployee;
                MySQLEmployeeManager mySqlEmployeeManager = new MySQLEmployeeManager();
                mySqlEmployeeManager.GetAddress(currentEmployee);
            }
        }

        private void InitUCEmployee()
        {
            this.employeeAdmin = new EmployeeAdmin();
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

        private bool checkRegex(System.Windows.Controls.TextBox valueTested, String regex)
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


        private async void InitLUCEmployee()
        {
            this.employeeAdmin.ucEmployeeList.LoadItem((await employeeManager.Get()).ToList());
        }

        private void InitActionsEmployee()
        {
            this.employeeAdmin.ucEmployee.btnAddress.Click += BtnAddressEmployee_Click;
            this.employeeAdmin.btnAddEmployee.Click += btnAddEmployee_Click;
            this.employeeAdmin.btnSaveEmployee.Click += btnUpdateEmployee_Click;
            this.employeeAdmin.btnDelEmployee.Click += btnDelEmployee_Click;
            this.employeeAdmin.menuDuplicate.Click += MenuDuplicate_OnClick;
            this.employeeAdmin.menuDelete.Click += MenuDelete_OnClick;
            this.employeeAdmin.ucEmployeeList.ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            //this.employeeAdmin.ucEmployee.DatePHiring.KeyDown += disableTypingDatePHiring;
            //this.employeeAdmin.ucEmployee.DatePBirth.KeyDown += disableTypingDatePBirth;
        }

        private void BtnJobs_Click()
        {

        }

        public void LoadAddressPage(AddressAdmin addressAdmin)
        {
            this.addressAdmin = addressAdmin;
        }

        private void BtnAddressEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee.Address != null)
            {
                this.employeeAdmin.NavigationService.Navigate(new AddressAdmin());

                /*AddressAdmin addressAdmin = new AddressAdmin();
                Window window = new Window();
                window.Content = addressAdmin;
                window.Show();
                addressAdmin.UCAddress.Address = currentEmployee.Address;*/
            }
            else
            {
                System.Windows.MessageBox.Show("Can't open because address is null");
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
                System.Windows.MessageBox.Show("Birth > Hiring or (Hiring - Birth) < 18");
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
                        System.Windows.MessageBox.Show(ListName[index] + " is not valid.");
                        output = false;
                    }
                }
            }

            return output;
        }

        public void clearTextBBg(List<System.Windows.Controls.TextBox> listTextBoxs)
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
        #endregion
    }
}
#endregion