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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DubKing.ViewModel;
using DubKing.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DubKing.View.VoiceLibrary
{
    /// <summary>
    /// Interaction logic for VoiceTalentBar.xaml
    /// </summary>
    public partial class VoiceTalentBar : UserControl, INotifyPropertyChanged
    {
        Visibility _visibitityButton;


        public Visibility VisibilityButton
        {
            get { return _visibitityButton; }
            set
            {
                if (_visibitityButton == value) return;
                _visibitityButton = value;
                OnPropertyChanged();
            }
        }

        public VoiceTalentBar()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(params string[] properties)
        {
            if (PropertyChanged == null) return;

            foreach (var prop in properties)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ICommand NewKeywordCommand
        {
            get { return (ICommand)this.GetValue(NewKeywordCommandProperty); }
            set { this.SetValue(NewKeywordCommandProperty, value); }
        }
        public static readonly DependencyProperty NewKeywordCommandProperty = DependencyProperty.Register(
          "NewKeywordCommand", typeof(ICommand), typeof(VoiceTalentBar), new PropertyMetadata(null));
        public ICommand DeleteKeywordCommand
        {
            get { return (ICommand)this.GetValue(DeleteKeywordCommandProperty); }
            set { this.SetValue(DeleteKeywordCommandProperty, value); }
        }
        public static readonly DependencyProperty DeleteKeywordCommandProperty = DependencyProperty.Register(
          "DeleteKeywordCommand", typeof(ICommand), typeof(VoiceTalentBar), new PropertyMetadata(null));

        public IEnumerable<object> VLKeywordsList
        {
            get { return (IEnumerable<object>)this.GetValue(VLKeywordsListProperty); }
            set { this.SetValue(VLKeywordsListProperty, value); }
        }
        public static readonly DependencyProperty VLKeywordsListProperty = DependencyProperty.Register(
          "VLKeywordsList", typeof(IEnumerable<object>), typeof(VoiceTalentBar), new PropertyMetadata(null));
    }
}
