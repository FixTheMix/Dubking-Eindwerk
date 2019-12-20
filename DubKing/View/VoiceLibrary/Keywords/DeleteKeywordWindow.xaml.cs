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

namespace DubKing.View.VoiceLibrary.Keywords
{
    /// <summary>
    /// Interaction logic for DeleteKeywordWindow.xaml
    /// </summary>
    public partial class DeleteKeywordWindow : Window
    {
        private DeleteKeywordMessage _deleteKeywordMessage;
        public DeleteKeywordWindow(DeleteKeywordMessage msg)
        {
            InitializeComponent();
            _deleteKeywordMessage = msg;
            keywordsListBox.DataContext = msg;
        }
        public void GetKeyword()
        {
            this.ShowDialog();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _deleteKeywordMessage.IsCanceled = false;
            this.Close();
        }
    }
}
