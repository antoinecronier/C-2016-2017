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
using wpfzoo.entities;

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Logique d'interaction pour ZooUserControl.xaml
    /// </summary>
    public partial class ZooUserControl : UserControl
    {
        public ZooUserControl()
        {
            InitializeComponent();
            Zoo zoo = new Zoo();
            zoo.Name = "Zoo Tycoon";
            zoo.Birth = DateTime.Now;

            this.label.Content = "Name";
            this.textBox.Text = zoo.Name;

            this.label1.Content = "Birth";
            this.textBox1.Text = zoo.Birth.ToString();
        }
    }
}
