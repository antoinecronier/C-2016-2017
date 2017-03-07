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
using PokemonLib.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PokemonPrinter.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour ListPokemonUserControl.xaml
    /// </summary>
    public partial class ListPokemonUserControl : UserControl
    {
        #region attributs
        #endregion

        #region properties
        public ListView Pokedex { get; set; }
        public ObservableCollection<Pokemon> Obs { get; set; }
        #endregion

        #region constructor
        public ListPokemonUserControl()
        {
            this.InitializeComponent();
            Obs = new ObservableCollection<Pokemon>();
            this.pokedex.ItemsSource = Obs;
            this.Pokedex = this.pokedex;
        }
        #endregion

        #region methods
        /// <summary>
        /// Current list for User items.
        /// </summary>
        public void LoadItem(List<Pokemon> items)
        {
            Obs.Clear();
            foreach (var item in items)
            {
                Obs.Add(item);
            }
        }
        #endregion

        #region events
        #endregion
    }
}
