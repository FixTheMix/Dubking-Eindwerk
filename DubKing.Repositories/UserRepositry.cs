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
    class UserRepositry : IRepository<User>
    {
        #region Fields
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        #endregion
        public User Create(User item)
        {
            string sqlInsert = "BEGIN TRAN BEGIN TRY INSERT INTO Users(UserName, Password, ProjectAccess, VoiceLibraryAccess, ScheduleAccess, SettingsAccess) VALUES(@UserName, @Password, @ProjectAccess, @VoiceLibraryAccess, @ScheduleAccess, @SettingsAccess) SELECT UserID As Id, UserName, Password, ProjectAccess, VoiceLibraryAccess, ScheduleAccess, SettingsAccess  FROM Users WHERE UserID = SCOPE_IDENTITY() COMMIT END TRY BEGIN CATCH END CATCH";
            User user = new User();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    user = connection.QuerySingle<User>(sqlInsert, item);
                }
                return user;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public void Delete(User deletedItem)
        {
            string sql = "DELETE FROM ProjectUsers WHERE UserID = @Id;";
            string sql2 = "DELETE FROM Users WHERE UserId = @Id;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql, deletedItem);
                    connection.Execute(sql2, deletedItem);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }

        public List<User> GetAll()
        {
            string sql = "Select UserID AS Id, UserName, Password, ProjectAccess, VoiceLibraryAccess, ScheduleAccess,SettingsAccess From Users";
            List<User> users = new List<User>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    users = connection.Query<User>(sql).ToList<User>();
                }
                return users;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public User GetById(int id)
        {
            string sql = "SELECT UserID, UserName, Password, ProjectAccess, VoiceLibraryAccess, ScheduleAccess,SettingsAccess FROM Users WHERE UserId = @Id";
            User user = new User();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    user = connection.QueryFirstOrDefault<User>(sql, new { Id = id });
                }
                return user;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public void Update(User updatedItem)
        {
            string sql = "UPDATE Users SET UserName = @UserName, Password = @Password, ProjectAccess = @ProjectAccess, VoiceLibraryAccess = @VoiceLibraryAccess, ScheduleAccess = @ScheduleAccess, SettingsAccess = @SettingsAccess WHERE UserId = @Id";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(sql, updatedItem);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }
    }
}
