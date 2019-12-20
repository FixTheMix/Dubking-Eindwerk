using DubKing.Helpers;
using DubKing.Model;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Windows;

namespace DubKing.View
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    /// 

    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();
            Messenger.Default.Register<MessageCloseNewUserWindow>(this, CloseWindow);
        }

        private void CloseWindow(MessageCloseNewUserWindow message)
        {
            Messenger.Default.Unregister<MessageCloseNewUserWindow>(this);
            this.Close();
        }
    }
}
