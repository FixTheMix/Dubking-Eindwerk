using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories.Contracts
{
    public interface IEpisodeRepository
    {
        List<Episode> GetEpisodes(Project project);
        Episode Save(Episode episode);
        void Update(Episode episode);
        void Delete(Episode epsiode);
    }
}
