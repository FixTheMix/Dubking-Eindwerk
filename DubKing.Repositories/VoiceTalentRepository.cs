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
using System.Transactions;
using System.Windows;

namespace DubKing.Repositories
{
    public class VoiceTalentRepository : IRepository<VoiceTalent>
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private readonly ILanguageRepository _languageRepository;
        private readonly IRepository<VLKeyword> _keywordRepository;

        public VoiceTalentRepository(ILanguageRepository languageRepository, IRepository<VLKeyword> keywordRepository)
        {
            _languageRepository = languageRepository;
            _keywordRepository = keywordRepository;
        }
        public VoiceTalent Create(VoiceTalent item)
        {
            using (var transaction = new TransactionScope())
            {
                UpdateLanguage(item);

                string sqlAdress = "INSERT INTO Adresses (Street, Number, PoBox, City, PostalCode, Country) VALUES (@Street, @Number, @PoBox, @City, @PostalCode, @Country); SELECT SCOPE_IDENTITY() AS Id;";

                string sqlVoice = "INSERT INTO VoiceTalents (FirstName , SurName, Birthday, Email, AdressID, Expirience, Comment, Rating, Gender, PicturePath, HomeNumber, WorkNumber, MobileNumber,";

                if (item.LanguageId != 0)
                {
                    sqlVoice = sqlVoice + ", LanguageId";
                }

                    sqlVoice = sqlVoice + ") VALUES (@FirstName , @SurName, @Birthday, @Email, @AdressID, @Expirience, @Comment, @Rating, @Gender, @PicturePath, @HomeNumber, @WorkNumber, @MobileNumber";

                if (item.LanguageId != 0)
                {
                    sqlVoice = sqlVoice + ", @LanguageId";
                }

                sqlVoice = sqlVoice + "); SELECT SCOPE_IDENTITY() AS Id;";

                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        Identity adresId;
                        if (item.Adress != null)
                        {
                            adresId = connection.QuerySingle<Identity>(sqlAdress, item.Adress);
                        }
                        else
                        {
                            adresId = new Identity(); ;
                        }

                        var id = connection.QuerySingle<Identity>(sqlVoice, new { FirstName = item.FirstName, SurName = item.SurName, Birthday = item.Birthday, Email = item.Email, AdressID = adresId.Id, Expirience = item.Expirience, Comment = item.Comment, Rating = item.Rating, Gender = item.Gender, HomeNumber = item.HomeNumber, WorkNumber = item.WorkNumber, MobileNumber = item.MobileNumber, PicturePath = item.Picture, LanguageId = item.LanguageId });
                        item.VoiceId = id.Id;

                        UpdateKeywords(item);

                        transaction.Complete();
                        return item;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                    return null;
                }
            }
        }
        public void Delete(VoiceTalent deletedItem)
        {
            string sql = "DELETE FROM VoiceTalents WHERE VoiceID = @Id;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql, new { Id = deletedItem.VoiceId });
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }
        public List<VoiceTalent> GetAll()
        {
            string sql = @" 
                            SELECT v.VoiceID As VoiceId, v.FirstName, v.SurName, v.Birthday, v.Email, v.Expirience, v.Comment, v.Rating, v.Gender,v.HomeNumber, v.WorkNumber, v.MobileNumber, v.PicturePath AS Picture, v.IsActive, v.LanguageId, l.LanguageName AS Language ,a.AdressID, a.Street, a.Number, a.PoBox, a.City, a.PostalCode, a.Country, k.VlKeyId as KeywordId, k.VlKeyword as Keyword
                            FROM VoiceTalents v 
                            LEFT OUTER JOIN Languages l ON l.LanguageID = v.LanguageId 
                            LEFT OUTER JOIN Adresses a ON a.AdressID = v.AdressID
                            LEFT OUTER JOIN VoiceTalentsVlKeywords vkw ON vkw.VoiceId = v.VoiceID
                            LEFT OUTER JOIN VlKeywords k ON k.VlKeyId = vkw.VlKeyId ORDER BY v.FirstName, v.SurName;
                                ";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var voiceTalents = new Dictionary<int, VoiceTalent>();
                    connection.Query(sql, (Func<VoiceTalent, Adress, VLKeyword, VoiceTalent>)((voiceTalent, adress, vLKeyword) =>
                    {
                        VoiceTalent vt;
                        if (!voiceTalents.TryGetValue(voiceTalent.VoiceId, out vt))
                        {
                            voiceTalents.Add(voiceTalent.VoiceId, vt = voiceTalent);
                        }

                        if (adress != null)
                        {
                            vt.Adress = adress;
                        }

                        if (vLKeyword != null)
                        {
                            if (vt.Keywords == null)
                            {
                                vt.Keywords = new List<VLKeyword>();
                            }
                            if (vt.Keywords != null)
                            {
                                if (!vt.Keywords.Any(_ => _.KeywordId == vLKeyword.KeywordId))
                                {
                                    vt.Keywords.Add(vLKeyword);
                                }
                            }
                        }
                        return vt;

                    }), splitOn: "AdressID, KeywordId");

                    return voiceTalents.Select(_ => _.Value).ToList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }
        public VoiceTalent GetById(int id)
        {
            string sql = @" SELECT VoiceTalents.VoiceID As Id, VoiceTalents.FirstName, VoiceTalents.SurName, VoiceTalents.Birthday, VoiceTalents.Email, VoiceTalents.AdressID, VoiceTalents.Expirience, VoiceTalents.Comment, VoiceTalents.Rating, VoiceTalents.Gender, VoiceTalents.HomeNumber, VoiceTalents.WorkNumber, VoiceTalents.MobileNumber, VoiceTalents.PicturePath AS Picture, Languages.LanguageName AS Language, VoiceTalents.LanguageId 
                            FROM VoiceTalents 
                            LEFT OUTER JOIN Languages ON Languages.LanguageID = VoiceTalents.LanguageId
                            WHERE VoiceID = @Id";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.QuerySingleOrDefault(sql, new { id });
                    if (result != null)
                    {
                        var vt = new VoiceTalent
                        {
                            VoiceId = result.Id,
                            FirstName = result.FirstName,
                            SurName = result.SurName,
                            Birthday = result.Birthday,
                            Email = result.Email,
                            Adress = connection.QuerySingleOrDefault<Adress>("SELECT AdressID, Street, Number, PoBox, City, PostalCode, Country FROM Adresses WHERE AdressID = @AdressId", new { AdressId = result.AdressID }),
                            Expirience = result.Expirience,
                            Comment = result.Comment,
                            Rating = result.Rating,
                            Gender = (Gender)result.Gender,
                            MobileNumber = result.MobileNumber,
                            HomeNumber = result.HomeNumber,
                            WorkNumber = result.WorkNumber,
                            Picture = result.Picture,
                            Language = result.Language,
                        };
                        if (result.LanguageId != null)
                        {
                            vt.LanguageId = result.LanguageId;
                        }
                        return vt;
                    }
                    return null;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }
        public void Update(VoiceTalent updatedItem)
        {
            using (var transaction = new TransactionScope())
            {
                UpdateLanguage(updatedItem);
                UpdateKeywords(updatedItem);
                string sql = "UPDATE VoiceTalents SET FirstName = @FirstName, SurName = @SurName, BirthDay = @BirthDay, Email = @Email, Expirience = @Expirience, Comment = @Comment, Rating = @Rating, Gender = @Gender,HomeNumber = @HomeNumber, WorkNumber = @WorkNumber, MobileNUmber = @MobileNumber, PicturePath = @Picture, IsActive = @IsActive";
                if (updatedItem.LanguageId != 0)
                {
                    sql = sql + ", LanguageId = @LanguageId";
                }
                sql = sql + " WHERE VoiceID = @VoiceId;";

                string sql2 = "UPDATE Adresses SET Street = @Street, Number = @Number, PoBox = @PoBox, City = @City, PostalCode = @PostalCode, Country = @Country WHERE AdressID = @AdressId;";
                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Execute(sql, new { updatedItem.FirstName, updatedItem.SurName, updatedItem.Birthday, Email = updatedItem.Email, updatedItem.Expirience, updatedItem.Comment, updatedItem.Rating, updatedItem.Gender, updatedItem.HomeNumber, updatedItem.WorkNumber, updatedItem.MobileNumber, updatedItem.VoiceId, updatedItem.Picture, updatedItem.LanguageId, updatedItem.IsActive });

                        if (updatedItem.Adress != null)
                        {
                            connection.Execute(sql2, new { updatedItem.Adress.Street, updatedItem.Adress.Number, updatedItem.Adress.PoBox, updatedItem.Adress.City, updatedItem.Adress.PostalCode, updatedItem.Adress.Country, updatedItem.Adress.AdressId });
                        }

                        transaction.Complete();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                }

            }
        }
        private void UpdateKeywords(VoiceTalent item)
        {
            string deleteSql = "DELETE FROM VoiceTalentsVlKeywords WHERE VoiceId = @VoiceId";
            string insertSql = "INSERT INTO VoiceTalentsVlKeywords(VlKeyId, VoiceId) VALUES (@KeyId, @VoiceId)";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(deleteSql, item);
                    foreach (var kw in item.Keywords)
                    {
                        connection.Execute(insertSql, new { keyId = kw.KeywordId, VoiceId = item.VoiceId });
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }
        private void UpdateLanguage(VoiceTalent updatedItem)
        {
            if (updatedItem.LanguageId == 0 && !string.IsNullOrEmpty(updatedItem.Language))
            {
                updatedItem.LanguageId = _languageRepository.CreateLanguage(updatedItem.Language);
            }
        }
    }
}
