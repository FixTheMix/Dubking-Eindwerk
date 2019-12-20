using DubKing.Helpers;
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

namespace DubKing.View.Episode
{
    /// <summary>
    /// Interaction logic for ImportEpisode.xaml
    /// </summary>
    public partial class ImportEpisode : Window
    {
        public ImportEpisode()
        {
            InitializeComponent();
            Messenger.Default.Register<MessageCloseWindow>(this, OnCloseWindow);
        }
        private void OnCloseWindow(MessageCloseWindow messageCloseWindow)
        {
            this.Close();
        }
    }
}
