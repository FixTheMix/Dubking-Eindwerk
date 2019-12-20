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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DubKing.Controls.ProjectTable
{
    /// <summary>
    /// Interaction logic for ProjectTable.xaml
    /// </summary>
    public partial class ProjectTable : UserControl
    {
        ScrollViewer _rowheaderListBoxScroll;
        ScrollViewer _bodyListBoxScroll;
        ScrollViewer _columsHeaderListBoxScroll;
        public ProjectTable()
        {
            InitializeComponent();
            _rowheaderListBoxScroll = GetDescendantByType(HeaderListBox, typeof(ScrollViewer)) as ScrollViewer;
            _bodyListBoxScroll = GetDescendantByType(tableBody, typeof(ScrollViewer)) as ScrollViewer;
            _columsHeaderListBoxScroll = headerScroll;
            Messenger.Default.Register<RedrawTable>(this, Redraw);
        }

        private void Redraw(RedrawTable obj)
        {
            this.InvalidateVisual();
        }

        public Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }

        private void BodyScroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            _rowheaderListBoxScroll.ScrollToVerticalOffset(e.VerticalOffset);
            _bodyListBoxScroll.ScrollToVerticalOffset(e.VerticalOffset);
            _columsHeaderListBoxScroll.ScrollToHorizontalOffset(e.HorizontalOffset);
            _bodyListBoxScroll.ScrollToHorizontalOffset(e.HorizontalOffset);
        }

        private void BodyScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                _bodyListBoxScroll.ScrollToHorizontalOffset(_bodyListBoxScroll.HorizontalOffset + e.Delta);
                e.Handled = true;
            }
        }

        private void RowHeaders_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                _bodyListBoxScroll.ScrollToHorizontalOffset(_bodyListBoxScroll.HorizontalOffset + e.Delta);
                e.Handled = true;
            }
            else
            {
                _bodyListBoxScroll.ScrollToVerticalOffset(_bodyListBoxScroll.VerticalOffset - e.Delta);
                e.Handled = true;
            }
        }

        private void HeaderListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
