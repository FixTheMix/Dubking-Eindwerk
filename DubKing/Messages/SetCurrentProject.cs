using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class SetCurrentProject
    {
        public Project CurrentProject { get; set; }
        public SetCurrentProject(Project currentProject)
        {
            CurrentProject = currentProject;
        }
    }
}
