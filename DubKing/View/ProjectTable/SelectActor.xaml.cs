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

namespace DubKing.View.ProjectTable
{
    /// <summary>
    /// Interaction logic for SelectActor.xaml
    /// </summary>
    public partial class SelectActor : Window
    {
        public SelectActor()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseSelectActorWindow>(this, OnCloseMessage);
        }

        private void OnCloseMessage(CloseSelectActorWindow msg)
        {
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
