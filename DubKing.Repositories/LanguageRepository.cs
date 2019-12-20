using Dapper;
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
    class LanguageRepository : ILanguageRepository
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        public int CreateLanguage(string language)
        {
            string sql = @"IF NOT EXISTS (SELECT LanguageID FROM Languages WHERE LanguageName = @LanguageName)
                            BEGIN
                                INSERT INTO Languages(LanguageName) VALUES(@LanguageName);
                            END
                           SELECT LanguageID FROM Languages WHERE LanguageName = @LanguageName";

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var languageId = connection.QuerySingleOrDefault<int>(sql, new { LanguageName = language });
                    return languageId;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                    return 0;
                }
            }
        }

        public List<string> GetLanguages()
        {
            string sql = "SELECT LanguageName FROM Languages";
            try
            {
                List<string> languages = new List<string>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    languages = connection.Query<string>(sql).ToList();
                }
                return languages;

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }
    }
}
