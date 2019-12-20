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
    /// Interaction logic for NewKeywordWindow.xaml
    /// </summary>
    public partial class NewKeywordWindow : Window
    {
        private NewKeywordMessage _newKeywordMessage;
        public NewKeywordWindow(NewKeywordMessage msg)
        {
            InitializeComponent();
            _newKeywordMessage = msg;
            txtBox.DataContext = _newKeywordMessage;
        }

        public void GetNewKeyword()
        {
            this.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_newKeywordMessage.IsValid)
            {
                _newKeywordMessage.IsSaved = true;
                this.Close();
            }
        }
    }
}
