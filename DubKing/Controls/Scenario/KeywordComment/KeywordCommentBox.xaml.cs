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
    /// Interaction logic for KeywordCommentBox.xaml
    /// </summary>
    public partial class KeywordCommentBox : UserControl
    {
        public KeywordCommentBox()
        {
            InitializeComponent();
        }


        public bool HideButtons
        {
            get { return (bool)this.GetValue(HideButtonsProperty); }
            set { this.SetValue(HideButtonsProperty, value); }
        }
        public static readonly DependencyProperty HideButtonsProperty = DependencyProperty.Register(
          "HideButtons", typeof(bool), typeof(KeywordCommentBox), new PropertyMetadata(false));
    }
}
