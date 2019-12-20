using DubKing.ViewModel.ProjectTable;
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

namespace DubKing.Controls.ProjectTable
{
    /// <summary>
    /// Interaction logic for RowHeaderControl.xaml
    /// </summary>
    public partial class RowHeaderControl : UserControl
    {
        public RowHeaderControl()
        {
            InitializeComponent();
        }


        public ICommand OpenEditCharacter
        {
            get { return (ICommand)this.GetValue(OpenEditCharacterProperty); }
            set { this.SetValue(OpenEditCharacterProperty, value); }
        }
        public static readonly DependencyProperty OpenEditCharacterProperty = DependencyProperty.Register(
          "OpenEditCharacter", typeof(ICommand), typeof(RowHeaderControl), new PropertyMetadata(null));


        public ICommand OpenSelectActor
        {
            get { return (ICommand)this.GetValue(OpenSelectActorProperty); }
            set { this.SetValue(OpenSelectActorProperty, value); }
        }
        public static readonly DependencyProperty OpenSelectActorProperty = DependencyProperty.Register(
          "OpenSelectActor", typeof(ICommand), typeof(RowHeaderControl), new PropertyMetadata(null));



        public object ProjectTableDataContext
        {
            get { return (object)this.GetValue(ProjectTableDataContextProperty); }
            set { this.SetValue(ProjectTableDataContextProperty, value); }
        }
        public static readonly DependencyProperty ProjectTableDataContextProperty = DependencyProperty.Register(
          "ProjectTableDataContext", typeof(object), typeof(RowHeaderControl), new PropertyMetadata(null));
    }
}

