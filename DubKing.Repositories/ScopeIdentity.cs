using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dapper;
using DubKing.Model;

namespace DubKing.Repositories
{
    public static class ScopeIdentity
    {
        public static int GetScopeIdentity(SqlConnection connection)
        {
            try
            {
                var identity = connection.QuerySingle<Identity>("SELECT Scope_Identity() AS Id");
                return identity.Id;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return 0;
            }
        }
    }
}
