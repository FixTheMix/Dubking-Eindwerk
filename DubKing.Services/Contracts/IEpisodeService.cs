using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services.Contracts
{
    public interface IEpisodeService
    {
        IList<Episode> GetEpisodes(Project project);
        Episode SaveEpisode(Episode episode);
        void UpdateEpisode(Episode episode);
        void DeleteEpisode(Episode episode);
        Episode CheckStatus(Episode epsiode);
        IList<string> GetAllTranslators();
    }
}
