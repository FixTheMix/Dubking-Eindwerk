using DubKing.Model;
using DubKing.Model.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class LineCount
    {
        public bool Visible { get
            {
                if (Lines == 0)
                {
                    return false;
                }
                return true;
            } }
        public int Lines { get; set; }
        public int RecordedLines { get; set; }
        public string LineDisplay { get => NotRecordedLines.ToString() + "/" + Lines.ToString(); }
        public int NotRecordedLines { get => Lines - RecordedLines; }
        public double Ewl { get; set; }
        public double RecordedEwl { get; set; }
        public double NotRecorderEwl { get => Math.Round((Ewl - RecordedEwl), 2); }
        public string EwlDisplay { get => Math.Round(NotRecorderEwl).ToString() + "/" + Math.Round(Ewl).ToString(); }
        public double Avg { get => (Lines.ToDouble() + Ewl) / 2; }
        public double RecordedAvg { get => (RecordedLines.ToDouble() + RecordedEwl) / 2; }
        public double NotRecordedAvg { get => Math.Round((Avg - RecordedAvg), 2); }
        public string AvgDisplay { get => Math.Round(NotRecordedAvg).ToString() + "/" + Math.Round(Avg).ToString(); }
        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public LineCount()
        {

        }
        public LineCount(Episode episode)
        {
            Episode = episode;
        }
        public override string ToString()
        {
            return NotRecordedLines.ToString() + "/" + Lines.ToString();
        }
    }
}
