using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Helpers
{
    class MessageOpen<T>
    {
        private T _project;

        public MessageOpen(T project)
        {
            _project = project;
        }
        public T Project { get => _project; set => _project = value; }

    }
}
