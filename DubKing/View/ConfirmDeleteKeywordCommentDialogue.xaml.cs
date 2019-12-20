using DubKing.Messages;
using DubKing.Model;
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
    /// Interaction logic for ConfirmDeleteKeywordCommentDialogue.xaml
    /// </summary>
    public partial class ConfirmDeleteKeywordCommentDialogue : Window
    {
        private bool _isConfirmed;
        public ConfirmDeleteKeywordCommentDialogue()
        {
            InitializeComponent();
        }
        public void GetConfirmation(ConfirmGlossaryKeywordDeleteMessage key)
        {
            DataContext = key.Keyword;
            ShowDialog();
            key.CanDelete = _isConfirmed;
        }

        private void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            _isConfirmed = true;
            Close();
        }

        private void OnCancel_Click(object sender, RoutedEventArgs e)
        {
            _isConfirmed = false;
            Close();
        }
    }
}
