using DubKing.Model;
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

namespace DubKing.CustomControls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DubKing.CustomControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DubKing.CustomControls;assembly=DubKing.CustomControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CheckListBox/>
    ///
    /// </summary>
    public class CheckListBox : ListBox
    {
        static CheckListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckListBox), new FrameworkPropertyMetadata(typeof(CheckListBox)));
        }
        public static DependencyProperty BindableSelectedItemsProperty =
        DependencyProperty.Register("BindableSelectedItems",
            typeof(IList<VLKeyword>), typeof(CheckListBox),
            new FrameworkPropertyMetadata(default(IList<VLKeyword>),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBindableSelectedItemsChanged));

        public IList<VLKeyword> BindableSelectedItems
        {
            get => (IList<VLKeyword>)GetValue(BindableSelectedItemsProperty);
            set => SetValue(BindableSelectedItemsProperty, value);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            System.Collections.IList items = SelectedItems;
            var collection = items.Cast<VLKeyword>();
            BindableSelectedItems = collection.ToList();
        }

        private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CheckListBox listBox)
            {
                //listBox.SetSelectedItems(listBox.BindableSelectedItems);

                var selectedItems = new List<VLKeyword>();
                
                foreach (VLKeyword item in listBox.Items)
                {
                    if (listBox.BindableSelectedItems.Any(bK => bK.KeywordId == item.KeywordId))
                    {
                        selectedItems.Add(item);
                    }
                }
                listBox.SetSelectedItems(selectedItems);

                //listBox.SetSelectedItems(listBox.ItemsSource.Select(_ => _ as VLKeyword).Where(k => listBox.BindableSelectedItems.Any(bK => bK.KeywordId == k.KeywordId));
            }
                //
        }
    }
}
