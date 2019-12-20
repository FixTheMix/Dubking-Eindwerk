using Dapper;
using DubKing.Model;
using DubKing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DubKing.Repositories
{
    public class CharacterRepository : IRemoveUnused
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        private IRepository<VoiceTalent> _voiceRepository;

        public Character Create(Character item)
        {
            string sqlInsert = @"INSERT INTO Characters (ProjectID, CharName, Comment, Status) VALUES (@ProjectId, @CharName, @Comment, @Status);Select SCOPE_IDENTITY() AS Id;";

            try
            {

            
            using (var connection = new SqlConnection(_connectionString))
            {
                Identity id = connection.QuerySingle<Identity>(sqlInsert, new { ProjectId = item.Project.ProjectId, CharName = item.Name, Comment = item.Comment, Status = item.Status });
                item.CharacterId = id.Id;
            }
            
            return item;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public void Delete(Character deletedItem)
        {
            string sql = "DELETE FROM Characters WHERE CharacterId = @Id;";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql, new { Id = deletedItem.CharacterId });
                }
                RemoveUnused();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }

        public List<Character> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Character> GetAll(Project item)
        {
            RemoveUnused();

            try
            {

                string sql2 = @"SELECT c.CharacterID AS CharacterId, c.CharName AS Name, c.Comment,c.Status, c.VoiceTalentID AS VoiceId, v. FirstName, v.SurName, v.Birthday, v.Email,                          v.Expirience,v.Comment, v.Rating, v.Gender, l.LanguageID, v.PicturePath, v.MobileNumber, v.HomeNumber, v.WorkNumber, v.AdressID As AdressId,              a.Street, a.Number, a.PoBox,a.City,a.PostalCode,a.Country
                            FROM Characters c
                            LEFT OUTER JOIN VoiceTalents v ON c.VoiceTalentID = v.VoiceID
                            LEFT OUTER JOIN Languages l ON v.LanguageId = l.LanguageID
                            LEFT OUTER JOIN Adresses a ON v.AdressID = a.AdressID
                            WHERE c.ProjectID = @ProjectId
                            ORDER BY c.CharacterID";
                //List<Character> characters = new List<Character>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var characters = connection.Query<Character, VoiceTalent, Adress, Character>(sql2, (c, v, a) =>
                        {
                            if (a != null && v != null)
                            {
                                v.Adress = a;
                            }
                            if (v != null)
                            {
                                c.VoiceTalent = v;
                            }
                            c.Project = item;
                            return c;
                        }, new { item.ProjectId }, splitOn: "VoiceId, AdressID");
                    return characters.ToList<Character>();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }


        public Character GetById(int id)
        {
            string sql = "SELECT CharacterID AS Id, CharName AS Name, Comment, Status FROM Characters WHERE CharacterID = @Id";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var results = connection.QuerySingleOrDefault<Character>(sql, new { Id = id });
                    return results;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public void Update(Character updatedItem)
        {
            string sql;
            if (updatedItem.VoiceTalent.VoiceId != 0)
            {
                sql = "UPDATE Characters SET CharName = @Name, Comment = @Comment, Status = @Status, VoiceTalentID = @VoiceTalentID WHERE CharacterID = @Id;";
            }
            else
            {
                sql = "UPDATE Characters SET CharName = @Name, Comment = @Comment, Status = @Status WHERE CharacterID = @Id;";
            }


            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql, new { Name = updatedItem.Name, Comment = updatedItem.Comment, Status = updatedItem.Status, VoiceTalentID = updatedItem.VoiceTalent.VoiceId, Id = updatedItem.CharacterId });
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }

        public void RemoveUnused()
        {
            string sql = "DELETE FROM Characters WHERE CharacterID NOT IN (SELECT DISTINCT CharacterID FROM Lines) AND CharacterID NOT IN (SELECT DISTINCT OriginalCharacterID FROM Lines);";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }

        public Character Create(Character item, bool removeUnused = true)
        {
            if (removeUnused)
            {
                RemoveUnused();
            }
           return Create(item);
        }

        public CharacterRepository(IRepository<VoiceTalent> voiceRepository)
        {
            _voiceRepository = voiceRepository;
        }
    }
}
