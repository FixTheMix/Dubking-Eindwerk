using Dapper;
using DialogueService;
using DubKing.Repositories.Base;
using DubKing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DubKing.Repositories
{
    public class VoiceTalentDbOperations : RepositoryBase, IVoiceTalentDbOperations
    {
        public VoiceTalentDbOperations(IShowDialogue dialService, IConnectionStringReader connectionStringReader) : base(dialService, connectionStringReader)
        {
        }

        public void UpdateVoiceTalentsStatus()
        {
            string sql = @" UPDATE VoiceTalents SET IsActive = 1 WHERE VoiceId IN(
                            SELECT v.VoiceId
                            FROM VoiceTalents v 
                            LEFT OUTER JOIN Characters c ON c.VoiceTalentID = v.VoiceID
                            LEFT OUTER JOIN Lines l ON l.CharacterID = c.CharacterID
                            WHERE LineId IN (SELECT LineId FROM Lines WHERE IsRecorded = 0)
                            GROUP BY v.VoiceID);

                            UPDATE VoiceTalents SET IsActive = 0 WHERE VoiceId NOT IN(
                            SELECT v.VoiceId
                            FROM VoiceTalents v 
                            LEFT OUTER JOIN Characters c ON c.VoiceTalentID = v.VoiceID
                            LEFT OUTER JOIN Lines l ON l.CharacterID = c.CharacterID
                            WHERE LineId IN (SELECT LineId FROM Lines WHERE IsRecorded = 0)
                            GROUP BY v.VoiceID);";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var result = connection.Execute(sql);
                }
            }
            catch (SqlException ex)
            {
                //MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                ShowErrorDialogue(ex.Message);
            }
            
        }
    }
}
