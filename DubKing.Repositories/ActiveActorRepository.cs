using Dapper;
using DialogueService;
using DubKing.Model;
using DubKing.Repositories.Base;
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
    public class ActiveActorRepository : RepositoryBase, IReadAllRepository<ActiveActor>
    {
        public ActiveActorRepository(IShowDialogue dialService, IConnectionStringReader connectionStringReader) : base(dialService, connectionStringReader)
        {
        }

        public IEnumerable<ActiveActor> GetAll()
        {
            string sql = @" SELECT v.VoiceId, 
                            CONCAT(v.FirstName, ' ', v.SurName) As Name, 
                            COUNT(l.LineId) AS TotalLines,
                            SUM(CASE WHEN l.IsRecorded = 1 then 1 else 0 end) AS RecordedLines,
                            SUM(l.Ewl) As TotalEwl,
                            SUM(CASE WHEN l.IsRecorded = 1 THEN l.Ewl ELSE 0.0 END) AS RecodedEwl
                            FROM VoiceTalents v 
                            LEFT OUTER JOIN Characters c ON c.VoiceTalentID = v.VoiceID
                            LEFT OUTER JOIN Lines l ON l.CharacterID = c.CharacterID
                            WHERE v.IsActive = 1
                            GROUP BY v.VoiceID, CONCAT(v.FirstName, ' ', v.SurName)";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.Query(sql, (Func<ActiveActor, SimpleLineCount, ActiveActor>)((a,l)=>
                        {
                            a.LineCount = l;
                            return a;
                        }), splitOn:"TotalLines");
                    
                    return result;
                }
            }
            catch (SqlException ex)
            {
                ShowErrorDialogue(ex.Message);
                return new List<ActiveActor>();
            }

        }
    }
}
