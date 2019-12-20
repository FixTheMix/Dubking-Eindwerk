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
using System.Windows.Shapes;

namespace DubKing.View
{
    /// <summary>
    /// Interaction logic for ErrorMessageDialog.xaml
    /// </summary>
    public partial class ErrorMessageDialog : Window
    {
        public string DialogTitle { get; set; }
        public string MessageText { get; set; }
        public string ButtonText { get; set; }
        public ErrorMessageDialog(string title, string message, string buttonText)
        {
            InitializeComponent();
            DataContext = this;
            DialogTitle = title;
            MessageText = message;
            ButtonText = buttonText;
        }

        private void MessageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
