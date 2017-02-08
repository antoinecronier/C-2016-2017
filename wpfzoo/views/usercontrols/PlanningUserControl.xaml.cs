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
using wpfzoo.entities;

namespace wpfzoo.views.usercontrols
{
    /// <summary>
    /// Logique d'interaction pour PlanningUserControl.xaml
    /// </summary>
    public partial class PlanningUserControl : UserControl
    {
        #region attributs
        #endregion

        #region properties
        public ListView PlanningList { get; set; }
        #endregion

        #region constructor
        public PlanningUserControl()
        {
            this.InitializeComponent();

            //testcode
            //Dictionary<Schedule, Structure> planning = new Dictionary<Schedule, Structure>();
            //Schedule horaire = new Schedule();
            //horaire.Start = DateTime.Now;
            //horaire.End = DateTime.Now.AddHours(2);
            //Schedule nettoyage = new Schedule();
            //nettoyage.Start = DateTime.Now.AddHours(2);
            //nettoyage.End = nettoyage.Start.AddHours(0.2);
            //Structure cage = new Structure();
            //cage.Name = "Cajolions";
            //Structure toilettes = new Structure();
            //toilettes.Name = "Popo";

            //planning.Add(horaire, cage);
            //planning.Add(nettoyage, toilettes);

            //this.LoadItem(planning);
        }
        #endregion

        #region methods
        /// <summary>
        /// Current list for Planning items.
        /// </summary>
        public void LoadItem(Dictionary<Schedule, Structure> items)
        {
            Resources["Plannings"] = items;
        }
        #endregion

        #region events
        #endregion
    }
}
