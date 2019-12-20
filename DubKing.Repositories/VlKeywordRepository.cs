using Dapper;
using DubKing.Model;
using DubKing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Windows;

namespace DubKing.Repositories
{
    class VlKeywordRepository : IRepository<VLKeyword>
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        public VLKeyword Create(VLKeyword item)
        {
            string sql = @"IF NOT EXISTS (SELECT * FROM VlKeywords WHERE VlKeyword = @Keyword)
                            BEGIN 
                            INSERT INTO VlKeywords(VlKeyword) VALUES (@Keyword);
                            
                            END
                          SELECT VlKeyId as KeywordId, VlKeyword as Keyword FROM VlKeywords WHERE VlKeyword = @Keyword;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.QuerySingle<VLKeyword>(sql, item);
                    return result;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public void Delete(VLKeyword deletedItem)
        {
            string sql1 = "DELETE FROM VoiceTalentsVlKeywords WHERE VlKeyId = @KeywordId;";
            string sql2 = "DELETE FROM VlKeywords WHERE VlKeyId = @KeywordId;";
            try
            {
                using (var transaction = new TransactionScope())
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Execute(sql1, deletedItem);
                        connection.Execute(sql2, deletedItem);
                    }
                    transaction.Complete();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }

        public List<VLKeyword> GetAll()
        {
            string sql = "SELECT VlKeyId as KeywordId, VlKeyword as Keyword FROM VlKeywords";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.Query<VLKeyword>(sql);
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public VLKeyword GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(VLKeyword updatedItem)
        {
            throw new NotImplementedException();
        }
    }
}
