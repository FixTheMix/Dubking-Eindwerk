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

namespace DubKing.View.VoiceLibrary
{
    /// <summary>
    /// Interaction logic for NewVoiceTalent.xaml
    /// </summary>
    public partial class NewVoiceTalent : Window
    {
        public NewVoiceTalent()
        {
            InitializeComponent();
            Messenger.Default.Register<CloseNewVoiceTalentWindow>(this, OnClose);
        }

        private void OnClose(CloseNewVoiceTalentWindow obj)
        {
            Messenger.Default.Unregister<CloseNewVoiceTalentWindow>(this);
            this.Close();
        }
    }
}
