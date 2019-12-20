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
    public class EpisodeService : IEpisodeService
    {

        IEpisodeRepository _episodeRepository;
        IProjectService _projectService;
        List<Episode> _episodes;
       ILineRepository<Line, Episode> _lineRepository;
        private readonly ITranslatorRepository _translatorRepository;

        public IList<Episode> GetEpisodes(Project project)
        {
            return _episodeRepository.GetEpisodes(project);
        }
        public Episode SaveEpisode(Episode episode)
        {
            return _episodeRepository.Save(episode);
        }

        public void UpdateEpisode(Episode episode)
        {
            _episodeRepository.Update(episode);
        }

        public void DeleteEpisode(Episode episode)
        {
            _episodeRepository.Delete(episode);
        }

        public Episode CheckStatus(Episode episode)
        {
            if (episode.EpStatus == EpStatus.Recording)
            {
                var lines = _lineRepository.GetAll(episode);
                int notRecordedLines = lines.Where(l => !l.IsRecorded).Count();
                if (notRecordedLines == 0)
                {
                    episode.EpStatus = EpStatus.Mixing;
                    UpdateEpisode(episode);
                }
            }
            if (episode.EpStatus == EpStatus.Mixing)
            {
                var lines = _lineRepository.GetAll(episode);
                int notRecordedLines = lines.Where(l => !l.IsRecorded).Count();
                if (notRecordedLines != 0)
                {
                    episode.EpStatus = EpStatus.Recording;
                    UpdateEpisode(episode);
                }
            }
            return episode;
        }

        public IList<string> GetAllTranslators()
        {
            return _translatorRepository.GetAll();
        }

        public EpisodeService(IEpisodeRepository episodeRepository, IProjectService projectService, ILineRepository<Line, Episode> lineRepository, ITranslatorRepository translatorRepository)
        {
            _episodeRepository = episodeRepository;
            _projectService = projectService;
            _lineRepository = lineRepository;
            _translatorRepository = translatorRepository;
        }
    }
}
