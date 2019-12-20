using DubKing.Model;
using DubKing.Repositories.Contracts;
using DubKing.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IRemoveUnused _characterRepository;
        public Character Create(Character character, Project project)
        {
            character.Project = project;
            return _characterRepository.Create(character, true);
        }
        public void Delete(Character character)
        {
            _characterRepository.Delete(character);
        }

        public IList<Character> Get(Project project)
        {
            return _characterRepository.GetAll(project);
        }

        public IList<Character> Get(Episode episode)
        {
            throw new NotImplementedException();
        }

        public IList<Character> Get(VoiceTalent voiceTalent)
        {
            throw new NotImplementedException();
        }
        public Character GetByName( string characterName, Project project)
        {
            return _characterRepository.GetAll(project).Where(c => c.Name == characterName).FirstOrDefault();
        }

        public void Update(Character character)
        {
            _characterRepository.Update(character);
        }

        public void RemoveUnused()
        {
            _characterRepository.RemoveUnused();
        }

        public CharacterService(IRemoveUnused characterRepository)
        {
            _characterRepository = characterRepository;
        }
       
    }
}
