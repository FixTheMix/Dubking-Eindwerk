using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Helpers
{
    class CloseNewProjectWindow
    {
        public Project NewProject { get; set; }
        public CloseNewProjectWindow()
        {

        }
        public CloseNewProjectWindow(Project newProject)
        {
            NewProject = newProject;
        }
    }


}
