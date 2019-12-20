using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Helpers
{
    public class OpenDetails
    {
        private Project _project;

        public Project Project { get => _project; set => _project = value; }

        public OpenDetails()
        {

        }
        public OpenDetails(Project project)
        {
            _project = project;
        }
    }
}
