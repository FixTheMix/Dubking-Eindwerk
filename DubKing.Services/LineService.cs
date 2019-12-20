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
    public class LineService : ILineService
    {
        private readonly ILineRepository<Line, Episode> _lineRepository;
        private readonly IRemoveUnused _CharacterRepository;
        private readonly IEpisodeService _episodeService;
        private readonly IVoiceTalentService _voiceTalentService;
        List<Line> _lines;
        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public IList<Line> Get(Episode episode)
        {
            var characters = episode.Project.Characters;
            var lines = _lineRepository.GetAll(episode);
            foreach (Line line in lines)
            {
                line.Character = characters.SingleOrDefault(c => c.CharacterId == line.CharacterId);
                line.OriginalCharacter = characters.SingleOrDefault(c => c.CharacterId == line.OriginalCharacterId);
                if (line.Character == null)
                {
                    line.Character = _CharacterRepository.GetById(line.CharacterId);
                    //episode.Project.Characters.Add(line.Character);
                }
                if (line.OriginalCharacter == null)
                {
                    line.OriginalCharacter = _CharacterRepository.GetById(line.OriginalCharacterId);
                }
                line.Save();
            }
            return lines;
        }

        public void SaveLines(IList<Line> lines, Episode episode)
        {
            //Get Characters
            IList<Character> characters = _CharacterRepository.GetAll(episode.Project);

            while (lines.Count() > 0)
            {
                var line = lines.First();
                var linesForCharacter = lines.Where(l => l.Character.Name == line.Character.Name).ToList();
               
                Character character = characters.Where(c => line.Character.Name == c.Name).FirstOrDefault();
                if (character == null)
                {
                    character = _CharacterRepository.Create(line.Character);
                    characters.Add(character);
                }
                while (linesForCharacter.Count() > 0)
                {
                    var l = linesForCharacter.First();
                    lines.Remove(l);
                    l.Character = character;
                    _lineRepository.Create(l);
                    linesForCharacter.Remove(l);
                }
            }
        }



        public void UpdateLineCharacter(Line line, Character newCharacter)
        {
            line.Character = newCharacter;
            _lineRepository.Update(line);
            line.Save();
        }

        public void UpdateLine(Line line)
        {
            _lineRepository.Update(line);
        }

        public IList<Line> GetLazy(Episode episode)
        {
            var lines = _lineRepository.GetAll(episode);
            return lines;
        }

        public Line CreateLine(Line line)
        {
            return _lineRepository.Create(line);
        }

        public void Delete(Line line)
        {
            _lineRepository.Delete(line);
        }

        public void UpdateAllLines(Character oldCharacter, Character newCharacter, Episode episode, bool markAsRec = false)
        {
            _lineRepository.UpdateAllLines(oldCharacter, newCharacter, episode, markAsRec);
            if (newCharacter.VoiceTalent != null)
            {
                _voiceTalentService.Update(newCharacter.VoiceTalent);
            }
        }

        public void Duplicate(int duplicateCount, Character character, Episode from, Episode to, bool betweenRange)
        {
            var allEpisodes = _episodeService.GetEpisodes(character.Project).ToList();
            var episodesIds = new List<int>();
            if (betweenRange)
            {
                episodesIds = allEpisodes.Where(e => string.Compare(e.CustomCode, from.CustomCode) >= 0 && string.Compare(e.CustomCode, to.CustomCode) <= 0).Select(e => e.EpisodeId).ToList();
            }
            else
            {
                episodesIds = allEpisodes.Select(e => e.EpisodeId).ToList();
            }

            string characterName = character.Name;
            Character[] characters = new Character[duplicateCount];
            for (int i = 0; i < duplicateCount; i++)
            {
                if (i == 0 && !betweenRange)
                {
                    character.Name = characterName + $" {i + 1}";
                    characters[i] = character;
                }
                else
                {
                    
                    string newCharacterName = characterName + $" { i + 1}";
                    var newCharacter = character.Project.Characters.SingleOrDefault(_ => _.Name == newCharacterName && _.CharacterId != 0);
                    if (newCharacter == null)
                    {
                        newCharacter = new Character(character.Project) {Name = newCharacterName};
                    }
                    characters[i] = newCharacter;
                }
                
            }

            var lines = _lineRepository.GetAll(character).ToList();
            var editableLines = lines.Where(l => episodesIds.Contains(l.EpisodeID)).ToArray();
            var removableLines = lines.Where(l => episodesIds.Contains(l.EpisodeID)).ToArray();
            for (int i = 0; i < removableLines.Length; i++)
            {
                _lineRepository.Delete(removableLines[i]);
            }
            List<Line> results = new List<Line>();
            foreach (Line line in editableLines)
            {
                foreach (Character c in characters)
                {
                    results.Add(new Line()
                    {
                        Text = line.Text,
                        Comment = line.Comment,
                        CharacterId = c.CharacterId,
                        Character = c,
                        Timecode = line.Timecode,
                        EpisodeID = line.EpisodeID
                    });
                }
            }

            foreach (Character c in characters.Where(_=>_.CharacterId == 0))
            {
                _CharacterRepository.Create(c, false);
            }

            foreach (Line line in results.Where(l=> l.LineId == 0))
            {
                _lineRepository.Create(line);
            }

            //foreach (Line line in results.Where(l=> l.LineId != 0))
            //{
            //    _lineRepository.Update(line);
            //}
            
        }

        public void SplitLines(Character character, string seperator, Project project)
        {
            character.Project = project;
            string[] seperators = new string[] {seperator};
            string[] characterNames = character.Name.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            character.Name = characterNames[0];
            _CharacterRepository.Update(character);
            for (int j = 1; j < characterNames.Length; j++)
            {
                Character newCharacter = new Character(project) { Name = characterNames[j] };
                newCharacter = _CharacterRepository.Create(newCharacter);
                var lines = _lineRepository.GetAll(character).ToArray();
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].Character = newCharacter;
                    _lineRepository.Create(lines[i]);
                }
            }
        }

        public LineService(ILineRepository<Line, Episode> lineRepository, IRemoveUnused characterRepository, IEpisodeService episodeService, IVoiceTalentService voiceTalentService)
        {
            _lineRepository = lineRepository;
            _CharacterRepository = characterRepository;
            _episodeService = episodeService;
            _voiceTalentService = voiceTalentService;
            _lines = new List<Line>();
        }

    }
}
