using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DubKing.Controls
{
    /// <summary>
    /// Interaction logic for VoiceTalentFields.xaml
    /// </summary>
    public partial class VoiceTalentFields : UserControl
    {
        public VoiceTalentFields()
        {
            InitializeComponent();
        }
        public ObservableCollection<string> PosibleLanguages
        {
            get { return (ObservableCollection<string>)this.GetValue(PosibleLanguagesProperty); }
            set { this.SetValue(PosibleLanguagesProperty, value); }
        }
        public static readonly DependencyProperty PosibleLanguagesProperty = DependencyProperty.Register(
          "PosibleLanguages", typeof(ObservableCollection<string>), typeof(VoiceTalentFields), new PropertyMetadata(new ObservableCollection<string>()));

        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
        "Command",
        typeof(ICommand),
        typeof(UserControl));

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }

            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty DragEnterCommandProperty =
        DependencyProperty.Register(
        "DragEnterCommand",
        typeof(ICommand),
        typeof(UserControl));

        public ICommand DragEnterCommand
        {
            get
            {
                return (ICommand)GetValue(DragEnterCommandProperty);
            }

            set
            {
                SetValue(DragEnterCommandProperty, value);
            }
        }


        public ICommand NewKeywordCommand
        {
            get { return (ICommand)this.GetValue(NewKeywordCommandProperty); }
            set { this.SetValue(NewKeywordCommandProperty, value); }
        }
        public static readonly DependencyProperty NewKeywordCommandProperty = DependencyProperty.Register(
          "NewKeywordCommand", typeof(ICommand), typeof(VoiceTalentFields), new PropertyMetadata(null));
        public ICommand DeleteKeywordCommand
        {
            get { return (ICommand)this.GetValue(DeleteKeywordCommandProperty); }
            set { this.SetValue(DeleteKeywordCommandProperty, value); }
        }
        public static readonly DependencyProperty DeleteKeywordCommandProperty = DependencyProperty.Register(
          "DeleteKeywordCommand", typeof(ICommand), typeof(VoiceTalentFields), new PropertyMetadata(null));


        public IEnumerable<object> VLKeywordsList
        {
            get { return (IEnumerable<object>)this.GetValue(VLKeywordsListProperty); }
            set { this.SetValue(VLKeywordsListProperty, value); }
        }
        public static readonly DependencyProperty VLKeywordsListProperty = DependencyProperty.Register(
          "VLKeywordsList", typeof(IEnumerable<object>), typeof(VoiceTalentFields), new PropertyMetadata(null));
    }
}
