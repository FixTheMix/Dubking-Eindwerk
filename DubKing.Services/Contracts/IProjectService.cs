using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services.Contracts
{
    public interface IProjectService
    {
        List<Project> GetProjects();
        Project Get(int id);
        List<Project> GetProjects(User user);
        Project SaveProject(Project project);
        void DeleteProject(Project project);
        void UpdateProject(Project project);
    }
}
