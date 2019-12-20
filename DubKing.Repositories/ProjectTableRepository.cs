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
    public class ProjectTableRepository : IProjectTableRepository
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        public LineCount[] GetLineCounts(Project project)
        {
            string sql = @"SELECT Lines.EpisodeId As EpisodeID, Lines.CharacterID As CharacterID, COUNT(Lines.LineId) AS Lines, SUM(IIF(Lines.IsRecorded = 1, 1, 0)) As RecordedLines, 
                                  SUM(Lines.Ewl) AS EWL, SUM(IIF(Lines.IsRecorded = 1, Lines.Ewl, 0)) As RecordedEwl
                FROM Lines
                LEFT OUTER JOIN Episodes ON Lines.EpisodeID = Episodes.EpisodeID 
                WHERE Episodes.ProjectID = @ProjectID
                GROUP BY Lines.EpisodeID, Lines.CharacterID;";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    var result = connection.Query<LineCount>(sql, new { ProjectID = project.ProjectId });
                    return result.ToArray<LineCount>();
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
