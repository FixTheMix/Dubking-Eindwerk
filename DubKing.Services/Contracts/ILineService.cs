using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services.Contracts
{
    public interface ILineService
    {
        void AddLine(Line line);
        Line CreateLine(Line line);
        IList<Line> Get(Episode episode);
        void SaveLines(IList<Line> lines, Episode episode);
        void UpdateLineCharacter(Line line, Character newCharacter);
        void UpdateLine(Line line);
        IList<Line> GetLazy(Episode episode);
        void Delete(Line line);
        void UpdateAllLines(Character olsCharacter, Character newCharacter, Episode epsiode, bool markAsRec = false);
        void Duplicate(int duplicateCount, Character character, Episode from, Episode to, bool betweenRange);
        void SplitLines(Character character, string seperator, Project project);
    }
}
