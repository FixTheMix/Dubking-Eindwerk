using DubKing.Messages;
using GalaSoft.MvvmLight.Messaging;
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

namespace DubKing.View.Scenario
{
    /// <summary>
    /// Interaction logic for CalculateOffsetWindow.xaml
    /// </summary>
    public partial class CalculateOffsetWindow : Window
    {
        public CalculateOffsetWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseCalculateOffsetWindow>(this, CloseWindow);
        }

        private void CloseWindow(CloseCalculateOffsetWindow obj = null)
        {
            Messenger.Default.Unregister(this);
            this.Close();
        }

        private void OnClickCloseBtn(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

    }
}
