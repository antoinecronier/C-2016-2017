using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Interaction logic for ListJobUserControl.xaml
    /// </summary>
    public partial class ListJobUserControl : UserControl
    {

        public ListView ItemsList { get; set; }
        public ObservableCollection<Job> Obs { get; set; }
        private MySQLManager<Job> jobManager = new MySQLManager<Job>();

        public ListJobUserControl()
        {
            this.InitializeComponent();
            Obs = new ObservableCollection<Job>();
            this.itemList.ItemsSource = Obs;
            this.ItemsList = this.itemList;
            this.ItemsList.SelectionMode = SelectionMode.Single;
        }

        private void RemoveNutritionContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            Obs.Remove(ItemsList.SelectedItem as Job);  // remove the selected Item 
        }

        private void EditNutritionContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedIndex > -1)
            {
                var job = new Job();
                job = (Job)ItemsList.SelectedItem; // casting the list view 
                MessageBox.Show("You are in edit for Name:" + job.Name, "Nutrition", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        /// <summary>
        /// Current list for User items.
        /// </summary>
        public void LoadItem(List<Job> items)
        {
            Obs.Clear();
            foreach (var item in items)
            {
                Obs.Add(item);
            }
        }

        private void RemoveJobContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            Obs.Remove(ItemsList.SelectedItem as Job);
        }

        private async void DuplicateJobContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedIndex > -1)
            {
                var duplicateJob = new Job();
                duplicateJob = (Job)ItemsList.SelectedItem;
                duplicateJob.Id = 0;
                await jobManager.Insert(duplicateJob);

                this.LoadItem((await jobManager.Get()).ToList());

            }
        }

    }
}
