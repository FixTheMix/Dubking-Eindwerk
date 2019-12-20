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

namespace DubKing.Controls.Scenario.KeywordComment
{
    /// <summary>
    /// Interaction logic for KeywordCommentList.xaml
    /// </summary>
    public partial class KeywordCommentList : UserControl
    {
        public KeywordCommentList()
        {
            InitializeComponent();
        }

        public string SelectedText
        {
            get { return (string)this.GetValue(SelectedTextProperty); }
            set { this.SetValue(SelectedTextProperty, value); }
        }
        public static DependencyProperty SelectedTextProperty = DependencyProperty.Register(
          "SelectedText", typeof(string), typeof(KeywordCommentList), new PropertyMetadata(""));
    }
}
