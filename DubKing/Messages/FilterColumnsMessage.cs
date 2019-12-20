using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class FilterColumnsMessage
    {
        public IEnumerable<Episode> FilteredEpisodes { get; set; }
        public FilterColumnsMessage(IEnumerable<Episode> episodes)
        {
            FilteredEpisodes = episodes;
        }
    }
}
