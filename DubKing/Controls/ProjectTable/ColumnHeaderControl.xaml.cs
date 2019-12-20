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

namespace DubKing.Controls.ProjectTable
{
    /// <summary>
    /// Interaction logic for ColumnHeaderControl.xaml
    /// </summary>
    public partial class ColumnHeaderControl : UserControl
    {
        public ColumnHeaderControl()
        {
            InitializeComponent();
        }

        public object ProjectTableDataContext
        {
            get { return (object)this.GetValue(ProjectTableDataContextProperty); }
            set { this.SetValue(ProjectTableDataContextProperty, value); }
        }
        public static readonly DependencyProperty ProjectTableDataContextProperty = DependencyProperty.Register(
          "ProjectTableDataContext", typeof(object), typeof(ColumnHeaderControl), new PropertyMetadata(null));
    }
}
