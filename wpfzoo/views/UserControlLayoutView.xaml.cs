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
using wpfzoo.entities.enums;
using wpfzoo.json;

namespace wpfzoo.views
{
    /// <summary>
    /// Logique d'interaction pour UserControlLayoutView.xaml
    /// </summary>
    public partial class UserControlLayoutView : Page
    {
        public UserControlLayoutView()
        {
            InitializeComponent();


            Animal animal = new Animal();
            animal.Name = "moufassa";
            animal.Gender = Gender.FEMALE;
            animal.Birth = DateTime.Now;
            animal.Height = 1.2M;
            animal.Weight = 120;
            animal.IsDead = true;
            this.animalUC.Animal = animal;

            StreetNumber streetNumber = new StreetNumber();
            streetNumber.Number = 666;
            streetNumber.Suf = StreetAvaibleItems.TER;
            this.streetNumberUC.StreetNumber = streetNumber;

            Address address = new Address();
            address.City = "Rennes";
            address.Street = "Rue du fond";
            address.PostalCode = "35147";
            address.StreetNumber = streetNumber;
            this.addressUC.Address = address;

            Schedule schedule = new Schedule();
            schedule.Start = DateTime.Now;
            schedule.End = DateTime.Now;
            this.scheduleUC.Schedule = schedule;

            Job job = new Job();
            job.Name = "Soigneur";
            job.Salary = 25.5M;
            job.Schedule = schedule;
            this.jobUC.Job = job;

            Structure structure = new Structure();
            structure.AssignAnimals.Add(animal);
            structure.Name = "Cajolion";
            structure.Schedule = schedule;
            structure.Surface = 21.5F;
            this.structureUC.Structure = structure;

            Employee employee = new Employee();
            employee.Address = address;
            employee.Birth = DateTime.Now;
            employee.Firstname = "Juan";
            employee.Gender = Gender.MALE;
            employee.Hiring = DateTime.Now;
            employee.Jobs.Add(job);
            employee.Lastname = "Del Santos";
            employee.Planning.Add(schedule, structure);
            this.employeeUC.Employee = employee;

            Zoo zoo = new Zoo();
            zoo.Address = address;
            zoo.Birth = DateTime.Now;
            zoo.Name = "lebozoo";
            zoo.Staff.Add(employee);
            zoo.Structures.Add(structure);
            this.zooUC.Zoo = zoo;



            /* TODO RM */
            MySQLManager<Employee> manager = new MySQLManager<Employee>();
            //employee.Manager = employee;
            manager.Insert(employee);
        }
    }
}
