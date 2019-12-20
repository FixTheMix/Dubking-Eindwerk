using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories.Contracts
{
    public interface IProjectTableRepository
    {
        LineCount[] GetLineCounts(Project project);
    }
}
