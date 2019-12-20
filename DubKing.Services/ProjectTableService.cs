using DubKing.Model;
using DubKing.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DubKing.Repositories.Contracts;
using System.Windows;

namespace DubKing.Services
{
    public class ProjectTableService : IProjectTableService
    {
        private readonly ILineRepository<Line, Episode> _lineRepository;
        private readonly IEpisodeService _episodeService;
        private readonly ICharacterService _characterService;
        private readonly ILineService _lineService;
        private readonly IProjectTableRepository _projectTableRepository;


        public Project GetProjectTable(Project project)
        {
            //project.Episodes = _episodeService.GetEpisodes(project).ToArray();
            //project.Characters = _characterService.Get(project).ToArray();
            
            //var linecounts = _projectTableRepository.GetLineCounts(project);
            //var lineDictionary = new Dictionary<int, Dictionary<int, LineCount>>();
            //for (int i = 0; i < linecounts.Length; i++)
            //{
            //    if (!lineDictionary.ContainsKey(linecounts[i].CharacterId))
            //    {
            //        lineDictionary.Add(linecounts[i].CharacterId, new Dictionary<int, LineCount>());
            //    }
            //    lineDictionary[linecounts[i].CharacterId].Add(linecounts[i].EpisodeId, linecounts[i]);
            //}
            
            //project.LineCounts = lineDictionary;

            
            project = _lineRepository.GetAll(project);
            
            return project;
        }


        public ProjectTableService(IEpisodeService episodeService, ICharacterService CharacterService, ILineService lineService, IProjectTableRepository projectTableRepository, ILineRepository<Line, Episode> lineRepository)
        {
            _lineRepository = lineRepository;
            _episodeService = episodeService;
            _characterService = CharacterService;
            _lineService = lineService;
            _projectTableRepository = projectTableRepository;
        }
    }
}
