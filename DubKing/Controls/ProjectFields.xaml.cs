using DubKing.Model.Extentions;
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

namespace DubKing.Controls
{
    /// <summary>
    /// Interaction logic for ProjectFields.xaml
    /// </summary>
    public partial class ProjectFields : UserControl
    {
        public ProjectFields()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var input = sender as TextBox;
            if (input.Text != input.Text.TimeFormat())
            {
                input.Text = input.Text.TimeFormat();
            }
        }

        private void TextBox_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var input = sender as TextBox;
            input.SelectAll();
        }
    }
}
