using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class ActiveProject
    {
        private int _projectId;
            
        public int ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private SimpleLineCount _lineCount;

        public SimpleLineCount LineCount
        {
            get { return _lineCount; }
            set { _lineCount = value; }
        }

    }
}
