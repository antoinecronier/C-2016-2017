using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpfzoo.database.entitieslinks;
using wpfzoo.entities;
using wpfzoo.entities.enums;
using wpfzoo.views.administration;

namespace wpfzoo
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {

        }

        #region Testing

        
        private async void TestDB1()
        {
            #region AppDBTesting
            Schedule schedule1 = new Schedule();
            schedule1.Start = DateTime.Now;
            schedule1.End = DateTime.Now.AddDays(2);

            Schedule schedule2 = new Schedule();
            schedule2.Start = DateTime.Now;
            schedule2.End = DateTime.Now.AddDays(2);

            Job job1 = new Job();
            job1.Name = "job1";
            job1.Salary = 10000;
            job1.Schedule = schedule1;

            Job job2 = new Job();
            job2.Name = "job2";
            job2.Salary = 10000;
            job2.Schedule = schedule1;

            Job job3 = new Job();
            job3.Name = "job3";
            job3.Salary = 10000;
            job3.Schedule = schedule2;

            StreetNumber streetNumber = new StreetNumber();
            streetNumber.Number = 10;
            streetNumber.Suf = StreetAvaibleItems.NONE;

            Address address = new Address();
            address.City = "testCity";
            address.PostalCode = "35230";
            address.Street = "testStreet";
            address.StreetNumber = streetNumber;

            Employee employee = new Employee();
            employee.Address = address;
            employee.Birth = DateTime.Now;
            employee.Firstname = "firstname";
            employee.Lastname = "lastname";
            employee.Hiring = DateTime.Now;
            employee.Gender = Gender.MALE;
            employee.Manager = null;
            employee.Jobs.Add(job1);
            employee.Jobs.Add(job2);
            employee.Jobs.Add(job3);

            MySQLEmployeeManager empManager = new MySQLEmployeeManager();

            Employee test = await empManager.Insert(employee);


            #endregion
        }

        public async void TestDB2()
        {
            MySQLEmployeeManager empManager = new MySQLEmployeeManager();
            Employee emp = await empManager.Get(4);
            empManager.GetJobs(emp);
            empManager.GetAddress(emp);
        }

        #endregion
    }
}
