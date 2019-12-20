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

namespace DubKing.Controls.ActiveActors
{
    /// <summary>
    /// Interaction logic for ActiveActorControl.xaml
    /// </summary>
    public partial class ActiveActorControl : UserControl
    {
        public ActiveActorControl()
        {
            InitializeComponent();
        }

        public bool ShowDetails
        {
            get { return (bool)this.GetValue(showDetailsProperty); }
            set { this.SetValue(showDetailsProperty, value); }
        }

        public static readonly DependencyProperty showDetailsProperty = DependencyProperty.Register("ShowDetails", typeof(bool), typeof(ActiveActorControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private void ShowDetails_Click(object sender, MouseButtonEventArgs e)
        {
            ShowDetails = !ShowDetails;
        }
    }
}
