using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class OpenImportEpisodeWindow
    {
        public Project CurrentProject { get; set; }
        public OpenImportEpisodeWindow( Project currentProject)
        {
            CurrentProject = currentProject;
        }
    }
}
