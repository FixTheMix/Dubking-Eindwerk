using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Helpers
{
    class MessageCurrentProject
    {
        public Project Project { get; set; }
        public MessageCurrentProject(Project project)
        {
            Project = project;
        }
    }
}
