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
    public class ActiveActorService : IActiveActorService
    {
        private readonly IReadAllRepository<ActiveActor> _activeActorRepository;
        private readonly IReadActiveProjects _activeProjectRepository;
        private readonly IVoiceTalentDbOperations _voiceTalentDbOperations;

        public IEnumerable<ActiveActor> GetAll()
        {
            UpdateVoiceTalentStatus();
            return _activeActorRepository.GetAll();
        }
        public ActiveActor LoadProjects(ActiveActor actor)
        {
            actor.Projects = _activeProjectRepository.GetActiveProjects(actor).ToList();
            return actor;
        }
        private void UpdateVoiceTalentStatus()
        {
            _voiceTalentDbOperations.UpdateVoiceTalentsStatus();
        }

        public ActiveActorService(IReadAllRepository<ActiveActor> activeActorRepository, IReadActiveProjects activeProjectsRepository, IVoiceTalentDbOperations voiceTalentDbOperations)
        {
            _activeActorRepository = activeActorRepository;
            _activeProjectRepository = activeProjectsRepository;
            _voiceTalentDbOperations = voiceTalentDbOperations;
        }
    }
}
