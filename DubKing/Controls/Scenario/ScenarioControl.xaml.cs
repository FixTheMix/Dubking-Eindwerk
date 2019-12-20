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

namespace DubKing.Controls.Scenario
{
    /// <summary>
    /// Interaction logic for ScenarioControl.xaml
    /// </summary>
    public partial class ScenarioControl : UserControl
    {
        public ScenarioControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string test
        {
            get { return (string)this.GetValue(testProperty); }
            set { this.SetValue(testProperty, value); }
        }
        public static readonly DependencyProperty testProperty = DependencyProperty.Register(
          "test", typeof(string), typeof(ScenarioControl), new PropertyMetadata(""));

        public object Lines
        {
            get { return (object)this.GetValue(LinesProperty); }
            set { this.SetValue(LinesProperty, value); }
        }
        public static readonly DependencyProperty LinesProperty = DependencyProperty.Register(
          "Lines", typeof(object), typeof(ScenarioControl), new PropertyMetadata(null));
    }
}
