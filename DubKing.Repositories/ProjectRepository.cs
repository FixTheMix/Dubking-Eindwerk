using Dapper;
using DubKing.Model;
using DubKing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;

namespace DubKing.Repositories
{
    class ProjectRepository : IRepository<Project>
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        #region Fields
        IRepository<User> _userRepository;
        private readonly ILanguageRepository _languageRepository;
        #endregion

        private void UpdateLanguages(Project project)
        {
            if (project.OriginalLanguageId == 0 && !string.IsNullOrEmpty(project.OriginalLanguage))
            {
                project.OriginalLanguageId = _languageRepository.CreateLanguage(project.OriginalLanguage);
            }
            if (project.DubbedLanguageId == 0 && !string.IsNullOrEmpty(project.DubbedLanguage))
            {
                project.DubbedLanguageId = _languageRepository.CreateLanguage(project.DubbedLanguage);
            }
        }

        public Project Create(Project item)
        {
            UpdateLanguages(item);
            string sql;
            if (item.OriginalLanguageId == 0 || item.DubbedLanguageId == 0)
            {
                if (item.OriginalLanguageId == 0)
                {
                    if (item.DubbedLanguageId == 0)
                    {
                        sql = @"INSERT INTO Projects (Customer, Title, Comment, Framerate, AvgDuration, ProjectType)
                            VALUES (@Customer, @Title, @Comment,  @FrameRate, @AvgDuration, @ProjectType)";
                    }
                    else
                    {
                        sql = @"INSERT INTO Projects (Customer, Title, Comment, DubbedLanguage, Framerate, AvgDuration, ProjectType)
                            VALUES (@Customer, @Title, @Comment, @DubbedLanguageId, @FrameRate, @AvgDuration, @ProjectType)";
                    }
                }
                else
                {
                    sql = @"INSERT INTO Projects (Customer, Title, Comment,  DubbedLanguage, Framerate, AvgDuration, ProjectType)
                            VALUES (@Customer, @Title, @Comment, @DubbedLanguageId, @FrameRate, @AvgDuration, @ProjectType)";
                }
                
            }
            else
            {
                sql = @"INSERT INTO Projects (Customer, Title, Comment, OriginalLanguage, DubbedLanguage, Framerate, AvgDuration, ProjectType)
                            VALUES (@Customer, @Title, @Comment,@OriginalLanguageId, @DubbedLanguageId, @FrameRate, @AvgDuration, @ProjectType)";
            }

            sql = sql + "; SELECT SCOPE_IDENTITY() AS ProjectId;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    var id = connection.QuerySingleOrDefault(sql, item);
                    foreach (User user in item.AutherizedUsers)
                    {
                        connection.Execute("INSERT INTO ProjectUsers (ProjectId, UserID) VALUES (@Projectid, @UserId)", new { ProjectId = id.ProjectId, UserId = user.Id });
                    }
                    item.ProjectId = (int)id.ProjectId;
                }
                return item;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public void Delete(Project deletedItem)
        {
            
            string sql = @"DELETE FROM Lines WHERE LineId IN(
SELECT Lines.LineId FROM Lines
Left OUTER JOIN Episodes ON Episodes.EpisodeID = Lines.EpisodeID
LEFT OUTER JOIN Projects ON Projects.ProjectID = Episodes.ProjectID
WHERE Projects.ProjectID = @ProjectId);
DELETE FROM Glossary WHERE ProjectID = @ProjectID;
            DELETE FROM Episodes WHERE Episodes.ProjectID = @ProjectId;

            Delete FROM Characters WHERE Characters.ProjectID = @ProjectId;
            DELETE FROM ProjectUsers WHERE ProjectUsers.ProjectID = @ProjectId;
            DELETE FROM Projects WHERE ProjectID = @ProjectId;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql, deletedItem);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }

        public List<Project> GetAll()
        {
            string sql = "SELECT Projects.ProjectID, Projects.Customer, Projects.Title, Projects.Comment, Original.LanguageName AS OriginalLanguage, dubbed.LanguageName AS DubbedLanguage, Projects.OriginalLanguage AS OriginaLanguageId, Projects.DubbedLanguage AS DubbedLanguageId, Projects.Framerate, Projects.AvgDuration FROM Projects LEFT OUTER JOIN Languages dubbed ON Projects.DubbedLanguage = dubbed.LanguageID LEFT OUTER JOIN Languages Original ON Projects.OriginalLanguage = Original.LanguageID";
            string usersSql = "SELECT Users.UserID AS Id, Users.UserName, Users.Password, Users.ProjectAccess, Users.VoiceLibraryAccess, Users.ScheduleAccess, Users.SettingsAccess FROM Users " +
                " INNER JOIN ProjectUsers ON Users.USerID = ProjectUsers.UserID " +
                "WHERE ProjectUsers.ProjectID = @ProjectId";
            List<Project> projects = new List<Project>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    projects = connection.Query<Project>(sql).ToList();
                    for (int i = 0; i < projects.Count; i++)
                    {
                        projects[i].AutherizedUsers = connection.Query<User>(usersSql, new { ProjectId = projects[i].ProjectId }).ToList<User>();
                    }
                }
                return projects;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public Project GetById(int id)
        {
            string sql = "SELECT Projects.ProjectID, Projects.Customer, Projects.Title, Projects.Comment, Original.LanguageName AS OriginalLanguage, dubbed.LanguageName AS DubbedLanguage, Projects.Framerate, Projects.AvgDuration FROM Projects LEFT OUTER JOIN Languages dubbed ON Projects.DubbedLanguage = dubbed.LanguageID LEFT OUTER JOIN Languages Original ON Projects.OriginalLanguage = Original.LanguageID WHERE ProjectID = @Projectid";
            string usersSql = "SELECT Users.UserID AS Id, Users.UserName, Users.Password, Users.ProjectAccess, Users.VoiceLibraryAccess, Users.ScheduleAccess, Users.SettingsAccess FROM Users " +
                " INNER JOIN ProjectUsers ON Users.USerID = ProjectUsers.UserID " +
                "WHERE ProjectUsers.ProjectID = @ProjectId";

            using (var connection = new SqlConnection(_connectionString))
            {
                var project = connection.QuerySingleOrDefault<Project>(sql, new { ProjectId = id });
                project.AutherizedUsers = connection.Query<User>(usersSql, new { ProjectId = id }).ToList<User>();

                return project;
            }
            
        }

        public void Update(Project updatedItem)
        {
            UpdateLanguages(updatedItem);

            string sql;
            if (updatedItem.OriginalLanguageId == 0 || updatedItem.DubbedLanguageId == 0)
            {
                if (updatedItem.OriginalLanguageId == 0)
                {
                    if (updatedItem.DubbedLanguageId == 0)
                    {
                        sql = @"UPDATE Projects SET Customer = @Customer, Title = @Title, Comment = @Comment, OriginalLanguage = NULL, DubbedLanguage = NULL, Framerate = @FrameRate, AvgDuration = @AvgDuration WHERE ProjectID = @ProjectId"; ;
                    }
                    else
                    {
                        sql = @"UPDATE Projects SET Customer = @Customer, Title = @Title, Comment = @Comment, OriginalLanguage = NULL, DubbedLanguage = @DubbedLanguageId, Framerate = @FrameRate, AvgDuration = @AvgDuration WHERE ProjectID = @ProjectId";
                    }
                }
                else
                {
                    sql = @"UPDATE Projects SET Customer = @Customer, Title = @Title, Comment = @Comment, OriginalLanguage = @OriginalLanguageId, DubbedLanguage = NULL, Framerate = @FrameRate, AvgDuration = @AvgDuration WHERE ProjectID = @ProjectId";
                }

            }
            else
            {
                sql = @"UPDATE Projects SET Customer = @Customer, Title = @Title, Comment = @Comment, OriginalLanguage = @OriginalLanguageId, DubbedLanguage = @DubbedLanguageId, Framerate = @FrameRate, AvgDuration = @AvgDuration WHERE ProjectID = @ProjectId";
            }
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql, updatedItem);
                    connection.Execute("DELETE FROM ProjectUsers WHERE ProjectID = @ProjectId", new { ProjectId = updatedItem.ProjectId });
                    foreach (User user in updatedItem.AutherizedUsers)
                    {
                        connection.Execute("IF NOT EXISTS (SELECT ProjectUserID FROM ProjectUsers WHERE ProjectID = @ProjectId AND UserID = @UserId) " +
                            "BEGIN " +
                            "INSERT INTO ProjectUsers (ProjectId, UserID) VALUES (@ProjectId, @UserId) " +
                            "END ", new { ProjectId = updatedItem.ProjectId, UserId = user.Id });
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }


        #region Constructor
        public ProjectRepository(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }
        #endregion
    }
}
