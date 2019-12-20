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
    public class TranslatorRepository : ITranslatorRepository
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        public IList<string> GetAll()
        {
            string sql = "SELECT Distinct(TranslatedBy) AS Translator FROM Episodes;";

            try
            {
                var translators = new List<string>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.Query(sql);
                    foreach (var item in result)
                    {
                        translators.Add(item.Translator);
                    }
                    return translators;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }
    }
}
