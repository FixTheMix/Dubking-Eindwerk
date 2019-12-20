using DubKing.Helpers;
using DubKing.Model;
using DubKing.Services;
using DubKing.ViewModel;
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

namespace DubKing.View
{
    /// <summary>
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class NewProject : Window, IDisposable
    {
        public NewProject()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseNewProjectWindow>(this, OnCloseWindow);
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<CloseNewProjectWindow>(this, OnCloseWindow);
        }

        private void OnCloseWindow(CloseNewProjectWindow messageCloseWindow)
        {
            this.Close();
        }

    }
}
