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
    /// Interaction logic for EditCharacterWindow.xaml
    /// </summary>
    public partial class EditCharacterWindow : Window
    {
        public EditCharacterWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseEditCharacterWindow>(this, Close);
        }

        private void Close(CloseEditCharacterWindow obj)
        {
            Messenger.Default.Unregister(this);
            this.Close();
        }
    }
}
