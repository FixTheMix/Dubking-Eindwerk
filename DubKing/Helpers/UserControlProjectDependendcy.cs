using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DubKing.Helpers
{
    public class UserControlProjectDependendcy:UserControl
    {
        public Project Project
        {
            get { return (Project)this.GetValue(ProjectProperty); }
            set { this.SetValue(ProjectProperty, value); }
        }
        public static readonly DependencyProperty ProjectProperty = DependencyProperty.Register(
          "Project", typeof(Project), typeof(UserControlProjectDependendcy));
    }
}
