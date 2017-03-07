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
using PokemonPrinter.WebService;
using PokemonLib.Entities;
using PokemonPrinter.Manager;

namespace PokemonPrinter
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PokedexManager pokedexManager = new PokedexManager();
        public MainWindow()
        {
            InitializeComponent();

            LoadPokedex();
        }

        public async void LoadPokedex()
        {
            this.pokedexUC.LoadItem(await pokedexManager.GetWebBundle(30, true));
        }

        public async void Test()
        {
            WebServiceManager manager = new WebServiceManager();
            Pokemon bulbi = new Pokemon();
            bulbi = await manager.GetData<Pokemon>("pokemon/1/", new Pokemon());
        }
    }
}
