using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class EpisodeAdded
    {
        public Episode Episode { get; set; }
        public EpisodeAdded(Episode episode)
        {
            Episode = episode;
        }
    }
}
