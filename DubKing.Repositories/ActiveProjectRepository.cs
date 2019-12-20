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
    public class ActiveProjectRepository : IReadActiveProjects
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        public IEnumerable<ActiveProject> GetActiveProjects(ActiveActor actor)
        {
            string sql = @" SELECT p.ProjectID, 
                            CONCAT(p.Customer, ' - ', p.Title) AS Name,
                            COUNT(l.LineId) AS TotalLines,
                            SUM(CASE WHEN l.IsRecorded = 1 then 1 else 0 end) AS RecordedLines,
                            SUM(l.Ewl) As TotalEwl,
                            SUM(CASE WHEN l.IsRecorded = 1 THEN l.Ewl ELSE 0.0 END) AS RecodedEwl 
                            FROM Projects p 
                            LEFT OUTER JOIN Characters c ON c.ProjectID = p.ProjectID
                            LEFT OUTER JOIN Lines l ON l.CharacterID = c.CharacterID
                            WHERE c.VoiceTalentID = @VoiceId
                            GROUP BY p.ProjectID, CONCAT(p.Customer, ' - ', p.Title);";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.Query(sql, (Func<ActiveProject, SimpleLineCount, ActiveProject>)((p, l) =>
                    {
                        p.LineCount = l;
                        return p;
                    }),actor, splitOn: "TotalLines");

                    return result;
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
