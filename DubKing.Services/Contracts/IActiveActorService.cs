using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services.Contracts
{
    public interface IActiveActorService
    {
        IEnumerable<ActiveActor> GetAll();
        ActiveActor LoadProjects(ActiveActor actor);
    }
}
