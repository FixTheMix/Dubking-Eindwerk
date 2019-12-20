using DubKing.Model;
using DubKing.Repositories.Contracts;
using DubKing.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly ILanguageRepository _languageRepository;
        public List<Project> GetProjects()
        {
            return _projectRepository.GetAll();
        }
        public List<Project> GetProjects(User user)
        {
            List<Project> projects = new List<Project>();
            foreach (Project project in _projectRepository.GetAll())
            {
                foreach (User u in project.AutherizedUsers)
                {
                    if (u.Id == user.Id)
                    {
                        projects.Add(project);
                    }
                }
            }
            return projects;
        }
        public Project SaveProject(Project project)
        {
            

            return _projectRepository.Create(project);
            
        }
        public void DeleteProject(Project project)
        {
            _projectRepository.Delete(project);
        }

        public void UpdateProject(Project project)
        {
            project.IsUnique = _projectRepository.GetAll().Where(p => p.Customer == project.Customer && p.Title == project.Title && p.ProjectId != project.ProjectId).Count() == 0;
            if (project.IsValid)
            {
                _projectRepository.Update(project);
            }
        }

        public Project Get(int id)
        {
            return _projectRepository.GetById(id);
        }

        public ProjectService(IRepository<Project> projectRepository, ILanguageRepository languageRepository)
        {
            _projectRepository = projectRepository;
            _languageRepository = languageRepository;
        }

    }
}
