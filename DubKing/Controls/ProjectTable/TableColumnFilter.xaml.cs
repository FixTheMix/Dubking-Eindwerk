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
    /// Interaction logic for TableColumnFilter.xaml
    /// </summary>
    public partial class TableColumnFilter : UserControl
    {
        public TableColumnFilter()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
          "Title", typeof(string), typeof(TableColumnFilter), new PropertyMetadata(""));
        public bool IsSelected
        {
            get { return (bool)this.GetValue(IsSelectedProperty); }
            set { this.SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
          "IsSelected", typeof(bool), typeof(TableColumnFilter), new PropertyMetadata(false));
        public ICommand FilterCommand
        {
            get { return (ICommand)this.GetValue(FilterCommandProperty); }
            set { this.SetValue(FilterCommandProperty, value); }
        }
        public static readonly DependencyProperty FilterCommandProperty = DependencyProperty.Register(
          "FilterCommand", typeof(ICommand), typeof(TableColumnFilter), new PropertyMetadata(null));
    }
}
