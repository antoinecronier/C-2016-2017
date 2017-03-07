using PokemonLib.Entities;
using PokemonPrinter.WebService;
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

namespace PokemonPrinter
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// /pokemon/-id-pokemon-
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            test();
        }

        public async void test()
        {
            //Test with 1 Pokemon
            WebServiceManager manager = new WebServiceManager();
            Pokemon bulbi = new Pokemon();
            bulbi = await manager.GetData<Pokemon>("pokemon/1/", new Pokemon());

        }
    }
}
