using Dapper;
using DubKing.Model;
using DubKing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DubKing.Model.Extentions;
using System.Configuration;
using System.Diagnostics;
using System.Windows;

namespace DubKing.Repositories
{
    class EpisodeRepository : IEpisodeRepository
    {
        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        public void Delete(Episode episode)
        {
            string sql = "DELETE FROM Episodes Where EpisodeID = @Id";
            try
            {
                using (var trans = new TransactionScope())
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Execute("DELETE FROM Lines WHERE EpisodeID = @EpisodeId", episode);
                        connection.Execute("DELETE FROM Characters WHERE NOT EXISTS(SELECT * FROM Lines WHERE Lines.CharacterID = Characters.CharacterID OR Lines.OriginalCharacterID = Characters.CharacterID)");
                        connection.Execute(sql, new { Id = episode.EpisodeId });
                        trans.Complete();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }




        public List<Episode> GetEpisodes(Project project)
        {
           
            string sql = "SELECT e.EpisodeID AS EpisodeId,e.Number, e.Title, e.TranslatedBy AS Translator, e.Comment, e.CustomCodeToggle, e.CustomCode, e.EpStatus, e.Offset As OffsetValue, (SELECT FrameRate FROM Projects WHERE ProjectID = @ProjectID) As FrameRate  FROM Episodes e WHERE ProjectID = @ProjectId ORDER BY CustomCode";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    var result = connection.Query<Episode, TimecodeOffset, Episode>(sql, (episode, timecodeOffset) =>
                     {
                         episode.Offset = timecodeOffset;
                         return episode;
                     }, project, splitOn: "OffsetValue");


                    return result.Select(_ => { _.Project = project; return _; }).ToList<Episode>();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }
        }

        public Episode Save(Episode episode)
        {
            string sql = "INSERT INTO Episodes (Title,Number, TranslatedBy, Comment, CustomCodeToggle, CustomCode, ProjectID, EpStatus, Offset) VALUES (@Title,@Number, @Translator, @Comment, @CustomCodeToggle, @CustomCode, @ProjectId, @EpStatus, @Offset); " +
                "SELECT EpisodeId AS Id FROM Episodes WHERE EpisodeID = SCOPE_IDENTITY();";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var episodeId = connection.QuerySingleOrDefault<Identity>(sql, new { Title = episode.Title, Number = episode.Number, Translator = episode.Translator, Comment = episode.Comment, CustomCodeToggle = episode.CustomCodeToggle, CustomCode = episode.CustomCode, ProjectId = episode.Project.ProjectId, EpStatus = episode.EpStatus, Offset = episode.Offset.OffsetValue });
                    episode.EpisodeId = episodeId.Id;
                }
                return episode;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
                return null;
            }

}

        public void Update(Episode episode)
        {
            string sql = "UPDATE Episodes SET Title = @Title, TranslatedBy = @Translation, Comment = @Comment, CustomCodeToggle = @CustomCodeToggle, CustomCode = @CustomCode, ProjectID = @ProjectId, EpStatus = @EpStatus, Offset = @Offset WHERE EpisodeID = @Id;";
            try { 
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { Title = episode.Title, Translation = episode.Translator, Comment = episode.Comment, CustomCodeToggle = episode.CustomCodeToggle, CustomCode = episode.CustomCode, ProjectId = episode.Project.ProjectId, Id = episode.EpisodeId, EpStatus = episode.EpStatus, Offset = episode.Offset.OffsetValue });
            }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An Exception Has Occurred! {ex.Message}");
            }
        }
    }
}

