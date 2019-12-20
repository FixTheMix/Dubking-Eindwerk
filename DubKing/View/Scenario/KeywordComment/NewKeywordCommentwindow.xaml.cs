using DubKing.Messages;
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

namespace DubKing.View.Scenario.KeywordComment
{
    /// <summary>
    /// Interaction logic for NewKeywordCommentwindow.xaml
    /// </summary>
    public partial class NewKeywordCommentwindow : Window
    {
        public NewKeywordCommentwindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var input = DataContext as OpenNewKeywordMessage;
            if (!input.IsValid) return;
            input.IsSaved = true;
            this.Close();
        }
    }
}
