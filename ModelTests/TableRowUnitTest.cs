using System;
using System.Collections.Generic;
using DubKing.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class TableRowUnitTest
    {
        [TestMethod]
        public void DefaultedTest()
        {
            var tr = new TableRow(new Character());

            Assert.IsNotNull(tr.Episodes);
            Assert.AreEqual(tr.Episodes.Length, 0);
            Assert.AreEqual(tr.LineCounts.Length, 0);
        }
        [TestMethod]
        public void AddEpisodeTest()
        {
            var tr = new TableRow(new Character());

            var ep = new Episode() {EpisodeId = 125};

            tr.AddEpisode(ep);

            Assert.AreEqual(tr.Episodes.Length, 1);
            Assert.AreEqual(tr.LineCounts.Length, 1);

            for (int i = 0; i < tr.Episodes.Length; i++)
            {
                Assert.AreEqual(tr.Episodes[i].EpisodeId, tr.LineCounts[i].Episode.EpisodeId);
            }
        }
        [TestMethod]
        public void AddEpisodesTest()
        {
            var tr = new TableRow(new Character());

            var eps = new List<Episode>
            {
                new Episode(){EpisodeId = 123},
                new Episode(){EpisodeId = 854},
                new Episode(){EpisodeId = 65},
                new Episode(){EpisodeId = 12},
                new Episode(){EpisodeId = 8},
                new Episode(){EpisodeId = 95}
            };

            tr.Episodes = eps.ToArray();

            Assert.AreEqual(tr.Episodes.Length, 6);
            Assert.AreEqual(tr.LineCounts.Length, 6);

            for (int i = 0; i < tr.Episodes.Length; i++)
            {
                Assert.AreEqual(tr.Episodes[i].EpisodeId, tr.LineCounts[i].Episode.EpisodeId);
            }

        }
    }
}
