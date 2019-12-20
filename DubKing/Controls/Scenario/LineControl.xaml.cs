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

namespace DubKing.Controls.Scenario
{
    /// <summary>
    /// Interaction logic for LineControl.xaml
    /// </summary>
    public partial class LineControl : UserControl
    {
        public LineControl()
        {
            InitializeComponent();
        }


        public string SelectedText
        {
            get { return (string)this.GetValue(SelectedTextProperty); }
            set { this.SetValue(SelectedTextProperty, value); }
        }
        public static DependencyProperty SelectedTextProperty = DependencyProperty.Register(
          "SelectedText", typeof(string), typeof(LineControl), new PropertyMetadata(""));
        public bool IsRecorded
        {
            get { return (bool)this.GetValue(IsRecordedProperty); }
            set { this.SetValue(IsRecordedProperty, value); }
        }
        public static readonly DependencyProperty IsRecordedProperty = DependencyProperty.Register(
          "IsRecorded", typeof(bool), typeof(LineControl), new PropertyMetadata(false));
        public string CharacterName
        {
            get { return (string)this.GetValue(CharacterNameProperty); }
            set { this.SetValue(CharacterNameProperty, value); }
        }
        public static readonly DependencyProperty CharacterNameProperty = DependencyProperty.Register(
          "CharacterName", typeof(string), typeof(LineControl), new PropertyMetadata(""));

        public string TimeCode
        {
            get { return (string)this.GetValue(TimeCodeProperty); }
            set { this.SetValue(TimeCodeProperty, value); }
        }
        public static readonly DependencyProperty TimeCodeProperty = DependencyProperty.Register(
          "TimeCode", typeof(string), typeof(LineControl), new PropertyMetadata("00:00:00:00"));

        public string LineText
        {
            get { return (string)this.GetValue(LineTextProperty); }
            set { this.SetValue(LineTextProperty, value); }
        }
        public static readonly DependencyProperty LineTextProperty = DependencyProperty.Register(
          "LineText", typeof(string), typeof(LineControl), new PropertyMetadata(""));

        public IEnumerable<string> CharacterNamesList
        {
            get { return (IEnumerable<string>)this.GetValue(CharacterNamesListProperty); }
            set { this.SetValue(CharacterNamesListProperty, value); }
        }
        public static DependencyProperty CharacterNamesListProperty = DependencyProperty.Register(
          "CharacterNamesList", typeof(IEnumerable<string>), typeof(LineControl), new PropertyMetadata(null));

        private void Rtb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var input = sender as RichTextBox;
            if (input == null) return;
            SelectedText = input.Selection.Text;
        }
    }
}
