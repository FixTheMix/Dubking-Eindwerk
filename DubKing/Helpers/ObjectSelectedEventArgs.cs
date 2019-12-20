using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Helpers
{
    public delegate void ObjectSelected(object sender, ObjectSelectedEventArgs e);
    public class ObjectSelectedEventArgs: EventArgs
    {
        public object SelectedItem { get; set; }
        public ObjectSelectedEventArgs(object selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}
