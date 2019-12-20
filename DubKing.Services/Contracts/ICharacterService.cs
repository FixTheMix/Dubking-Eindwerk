using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services.Contracts
{
    public interface ICharacterService
    {
        IList<Character> Get(Project project);
        IList<Character> Get(Episode episode);
        IList<Character> Get(VoiceTalent voiceTalent);
        Character GetByName(string characterName, Project project);
        Character Create(Character characterName, Project project);
        void Delete(Character character);
        void Update(Character character);
        void RemoveUnused();
    }
}
