using Dapper;
using DubKing.Model;
using DubKing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DubKing.Repositories
{
    
    class LineRepository : ILineRepository<Line, Episode>
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private readonly IRemoveUnused _characterRepository;
        public Line Create(Line item)
        {
            string sql = "INSERT INTO Lines (EpisodeID, CharacterID, Tekst, TimeCode, IsRecorded, Comment, OriginalCharacterID, OriginalText, OriginalTimecode, Ewl) VALUES (@EpisodeID, @CharacterID, @Tekst, @TimeCode, @IsRecorded, @Comment, @CharacterID, @Tekst, @TimeCode, @Ewl); SELECT SCOPE_IDENTITY() AS Id;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    Identity id = connection.QuerySingle<Identity>(sql, new { EpisodeID = item.EpisodeID, CharacterID = item.CharacterId, Tekst = item.Text, TimeCode = item.Timecode.TotalFrames, IsRecorded = item.IsRecorded, Comment = item.Comment, Ewl = item.Ewl });
                    item.LineId = id.Id;
                }
                item.Save();
                return item;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public void Delete(Line deletedItem)
        {
            string sql = "DELETE FROM Lines Where LineId = @Id";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql, new { Id = deletedItem.LineId });
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }

        public List<Line> GetAll()
        {
            throw new NotImplementedException();
            
        }

        public IList<Line> GetAll(Episode item)
        {
            GetAll(item.Project);
            return item.Lines;
        }

        public Line GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Line updatedItem)
        {
            string sql = "UPDATE Lines SET EpisodeId = @EpisodeId, CharacterID = @CharacterId, Tekst = @Text, IsRecorded = @IsRecorded, TimeCode = @Timecode, Comment = @Comment, Ewl = @Ewl WHERE LineID = @Id;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    int effectedRows = connection.Execute(sql, new { EpisodeId = updatedItem.EpisodeID, CharacterId = updatedItem.CharacterId, Text = updatedItem.Text, IsRecorded = updatedItem.IsRecorded, Timecode = updatedItem.Timecode.TotalFrames, Comment = updatedItem.Comment, Id = updatedItem.LineId, Ewl = updatedItem.Ewl });

                    //MessageBox.Show(effectedRows.ToString());
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }

        public void UpdateAllLines(Character oldCharacter, Character newCharacter, Episode episode , bool markAsRec = false)
        {
            string sql = "Update Lines SET CharacterId = @NewCharacterID WHERE CharacterID = @OldCharacterID AND EpisodeID = @EpisodeID;";
            if (markAsRec)
            {
                sql = "Update Lines SET CharacterId = @NewCharacterID, IsRecorded = 1 WHERE CharacterID = @OldCharacterID AND EpisodeID = @EpisodeID;";
            }
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql, new { NewCharacterID = newCharacter.CharacterId, OldCharacterID = oldCharacter.CharacterId, EpisodeID = episode.EpisodeId });
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }

        public IList<Line> GetAll(Character character)
        {
            string sql = "SELECT * FROM Lines WHERE CharacterID = @CharacterId";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.Query(sql, new { CharacterId = character.CharacterId });

                    var lines = new List<Line>();
                    foreach (var row in result)
                    {

                        var l = new Line()
                        {
                            LineId = row.LineId,
                            Text = row.Tekst,
                            Timecode = new Timecode(character.Project.FrameRate)
                            {
                                Frame = row.TimeCode
                            },
                            EpisodeID = row.EpisodeID,
                            CharacterId = row.CharacterID,
                            IsRecorded = row.IsRecorded,
                            Comment = row.Comment,
                            OriginalCharacterId = row.OriginalCharacterID,
                            OriginalText = row.OriginalText,
                            OriginalTimecode = new Timecode(character.Project.FrameRate)
                            {
                                Frame = row.TimeCode
                            }
                        };
                        l.Save();
                        lines.Add(l);
                    }

                    return lines;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }

        }

        public Project GetAll(Project project)
        {
            string sql = @" 
                            SELECT l.LineId, l.Tekst AS Text, l.Recorded, l.TimeCode, l.IsRecorded, l.Comment,          l.OriginalText, l.OriginalTimecode, l.OriginalCharacterID As OriginalCharacterId, l.Ewl,
                            c.CharacterID AS CharacterId, c.CharName AS Name, c.Comment, c.Status, c.CharacterID AS CharacterId, c.CharName AS Name, c.Comment, c.Status,
                            e.EpisodeID As EpisodeId, e.Number, e.Title, e.TranslatedBy As Translator, e.Comment, e.CustomCodeToggle, e.CustomCode, EpStatus, e.Offset,
                            v.VoiceID As VoiceId, v.FirstName, v.SurName, v.Birthday, v.Email, v.Expirience, v.Comment, v.Rating, v.Gender, v.HomeNumber, v.WorkNumber, v.MobileNumber, v.PicturePath AS Picture, v.LanguageId, 
                             la.LanguageName AS Language ,
                             a.AdressID, a.Street, a.Number, a.PoBox, a.City, a.PostalCode, a.Country, 
                             k.VlKeyId as KeywordId, k.VlKeyword as Keyword
                             FROM Lines l
                            LEFT OUTER JOIN Characters c
                            ON l.CharacterID = c.CharacterID
                            LEFT OUTER JOIN Characters oc
                            ON l.OriginalCharacterID = oc.CharacterID 
                            LEFT OUTER JOIN Episodes e
                            ON l.EpisodeID = e.EpisodeID 
                            LEFT OUTER JOIN VoiceTalents v 
                            ON c.VoiceTalentID = v.VoiceID 
                            LEFT OUTER JOIN Languages la 
                            ON la.LanguageID = v.LanguageId 
                            LEFT OUTER JOIN Adresses a ON a.AdressID = v.AdressID
                            LEFT OUTER JOIN VoiceTalentsVlKeywords vkw ON vkw.VoiceId = v.VoiceID
                            LEFT OUTER JOIN VlKeywords k ON k.VlKeyId = vkw.VlKeyId 
                            WHERE e.ProjectID = @ProjectId ";


            project.ClearChildren();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    Dictionary<int, Episode> episodes = new Dictionary<int, Episode>();
                    Dictionary<int, Character> characters = new Dictionary<int, Character>();
                    Dictionary<int, VoiceTalent> voices = new Dictionary<int, VoiceTalent>();
                    Dictionary<int, Line> lines = new Dictionary<int, Line>();

                    var result = connection.Query<Line, Character, Character, Episode, VoiceTalent, Adress, VLKeyword, Line>(sql, (l, c, oc, e, v, a, k) =>
                       {
                           Line line;
                           if (!lines.ContainsKey(l.LineId))
                           {
                               lines.Add(l.LineId, l);
                           }
                           line = lines[l.LineId];


                           if (!episodes.ContainsKey(e.EpisodeId))
                           {
                               e.Project = project;
                               episodes.Add(e.EpisodeId, e);
                           }
                           line.Episode = episodes[e.EpisodeId];

                           if (!characters.ContainsKey(c.CharacterId))
                           {
                               c.Project = project;
                               characters.Add(c.CharacterId, c);
                           }
                           line.Character = characters[c.CharacterId];

                           line.OriginalCharacter = oc;

                           if (v != null)
                           {
                               VoiceTalent vt;
                               if (!voices.TryGetValue(v.VoiceId, out vt))
                               {
                                   voices.Add(v.VoiceId, vt = v);
                               }

                               if (vt.Adress == null)
                               {
                                   vt.Adress = a;
                               }

                               if (k != null)
                               {
                                   if (vt.Keywords == null)
                                   {
                                       vt.Keywords = new List<VLKeyword>();
                                   }
                                   if (vt.Keywords != null)
                                   {
                                       if (!vt.Keywords.Any(_ => _.KeywordId == k.KeywordId))
                                       {
                                           vt.Keywords.Add(k);
                                       }
                                   }
                               }

                               c.VoiceTalent = vt;
                           }


                           return line;
                       }, new { ProjectId = project.ProjectId }, splitOn: "CharacterId,CharacterId,EpisodeId,VoiceID,AdressID, KeywordId");

                    return project;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public LineRepository(IRemoveUnused characterRepository)
        {
            _characterRepository = characterRepository;
        }
    }
}
