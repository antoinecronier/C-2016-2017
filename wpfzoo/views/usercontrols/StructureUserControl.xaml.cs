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
    /// Logique d'interaction pour StructureUserControl.xaml
    /// </summary>
    public partial class StructureUserControl : UserControlBase
    {
        private Structure structure;

        public Structure Structure
        {
            get { return structure; }
            set {
                structure = value;
                base.OnPropertyChanged("Structure");
            }
        }

        public StructureUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}

