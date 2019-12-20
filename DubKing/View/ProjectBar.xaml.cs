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
using DubKing.Helpers;

namespace DubKing.View
{
    /// <summary>
    /// Interaction logic for ProjectBar.xaml
    /// </summary>
    public partial class ProjectBar : UserControl
    {
        public ProjectBar()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent TextBloxSelectedEvent = EventManager.RegisterRoutedEvent(
        "TextBloxSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ProjectBar));

        // Provide CLR accessors for the event
        public event RoutedEventHandler TextBloxSelected
        {
            add { AddHandler(TextBloxSelectedEvent, value); }
            remove { RemoveHandler(TextBloxSelectedEvent, value); }
        }

        // This method raises the Tap event
        void RaiseTextBloxSelectedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ProjectBar.TextBloxSelectedEvent);
            RaiseEvent(newEventArgs);
        }

        private void RaiseRoutedEventObjectSelected(object sender, RoutedEventArgs e)
        {
            RaiseTextBloxSelectedEvent();
        }
    }
}
