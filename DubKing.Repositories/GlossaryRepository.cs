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
    public class GlossaryRepository : IGlossaryRepository
    {
        private string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// Creates new Glossary Keyword in Database
        /// </summary>
        /// <param name="key">KeywordComment that has to be saves to database</param>
        /// <returns>null: When SqlException occurred, KeywordComment when saving whas succesfull</returns>
        public KeywordComment Create(KeywordComment key)
        {
            string sql = "INSERT INTO Glossary(ProjectID, Keyword, Comment) VALUES(@ProjectID, @Keyword, @Comment); SELECT SCOPE_IDENTITY() as GlossaryId;";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.QueryFirst<KeywordComment>(sql, new { ProjectID = key.Project.ProjectId, Keyword = key.Keyword, Comment = key.Comment });
                    key.GlossaryId = result.GlossaryId;
                    return key;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Delete Glossary Keyword From DB
        /// </summary>
        /// <param name="key">The Keyword that will be deleted</param>
        /// <returns>true; when opperation was succesfull, false when no lines where effected or SqlException occurred</returns>
        public bool Delete(KeywordComment key)
        {
            string sql = "DELETE FROM Glossary Where GlossaryId = @GlossaryId";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.Execute(sql, key);
                    if (result == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return false;
            }
        }

        public KeywordComment[] Get(Project project)
        {
            string sql = "SELECT GlossaryId, ProjectID as ProjectId, Keyword, Comment FROM Glossary WHERE ProjectID = @ProjectId";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.Query<KeywordComment>(sql, new { ProjectId = project.ProjectId });
                    if (result == null)
                    {
                        return null;
                    }
                    return result.ToArray();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public bool Update(KeywordComment key)
        {
            string sql = "UPDATE Glossary SET Keyword = @Keyword, Comment = @Comment Where GlossaryId = @GlossaryId";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.Execute(sql, key);
                    if (result == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return false;
            }
        }
    }
}
