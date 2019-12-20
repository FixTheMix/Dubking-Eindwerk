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
using DubKing.ViewModel;

namespace DubKing.View
{
    /// <summary>
    /// Interaction logic for ProjectsListView.xaml
    /// </summary>
    public partial class ProjectsListView : UserControl
    {
        public ProjectsListView()
        {
            InitializeComponent();
        }

        private void ProjectList_TextBloxSelected(object sender, RoutedEventArgs e)
        {
            var originalSender = (ProjectBar)e.OriginalSource;
            ProjectList.SelectedItem = originalSender.DataContext;
            e.Handled = true;
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            FilterBox.Text = "";
        }
    }
}
