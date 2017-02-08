using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using wpfzoo.json;

namespace wpfzoo.views.administration
{
    /// <summary>
    /// Logique d'interaction pour StructureAdmin.xaml
    /// </summary>
    public partial class StructureAdmin : Page, INotifyPropertyChanged
    {
        ObservableCollection<Structure> structureList = new ObservableCollection<Structure>();
        MySQLManager<Structure> StructureManager = new MySQLManager<Structure>();
        public StructureAdmin()
        {
            InitializeComponent();
            InitLists();
        }

        private async void InitLists()
        {
            this.UCstructureList.LoadItem((await StructureManager.Get()).ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        

        #region clicks
        private void listEmployees_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            this.oneInfos.Visibility = Visibility.Visible;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int intToDelete;
            bool isInt = Int32.TryParse(this.StructureToDeleteName.Text, out intToDelete);
            if (isInt)
            {
                foreach (Structure struc in this.UCstructureList.Obs)
                {
                    if (struc.Id == Int32.Parse(this.StructureToDeleteName.Text))
                    {
                        StructureManager.Delete(struc);
                        this.UCstructureList.Obs.Remove(struc);
                        break;
                    }
                }
            }
            


        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Structure newStructure = new Structure();
            newStructure.Name = this.StructureName.Text;
            String surfaceString = this.StructureSurface.Text;
            float leOut;
            if (float.TryParse(surfaceString, out leOut))
            {
                float surfaceFloat = float.Parse(surfaceString);
                newStructure.Surface = surfaceFloat;
            }
            else
            {
                //MessageBox.Show("echec du parse du float de la surface de la structure le message est long");
                newStructure.Surface = 0;
            }
            newStructure.Schedule = null;
            
            StructureManager.Insert(newStructure);
            this.UCstructureList.AddItem(newStructure);

            this.oneInfos.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
