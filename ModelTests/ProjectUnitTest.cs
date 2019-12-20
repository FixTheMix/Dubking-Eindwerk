using System;
using System.Linq;
using DubKing.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class ProjectUnitTest
    {
        [TestMethod]
        public void TestDefaults()
        {
            Project p = new Project();

            Assert.IsNotNull(p.Episodes);
            Assert.IsNotNull(p.Characters);
            Assert.IsNotNull(p.TableRows);
            //Assert.IsNotNull(p.TableRowDictionary);

            Assert.AreEqual(0, p.Episodes.Length);
            Assert.AreEqual(0, p.Characters.Length);
            Assert.AreEqual(0, p.TableRows.Values.Count);
            //Assert.AreEqual(0, p.TableRowDictionary.Keys.Count);
        }
        [TestMethod]
        public void AddEpisodeTest()
        {
            Project p = new Project() { ProjectId = 123 };

            Episode e = new Episode();
            p.AddEpisode(e);

            Assert.AreEqual(1, p.Episodes.Length);
            Assert.AreEqual(p.ProjectId, e.Project.ProjectId);
            Assert.AreSame(p, p.Episodes[0].Project);
            Assert.AreSame(p, e.Project);
            foreach (var character in p.TableRows.Keys)
            {
                Assert.AreSame(p.Episodes, p.TableRows[character].Episodes);
            }
            

        }
        [TestMethod]
        public void AddEpisodesTest()
        {
            Project p = new Project() { ProjectId = 123 };
            Episode[] eps = new Episode[]
            {
                new Episode() { EpisodeId = 1},
                new Episode() { EpisodeId = 2},
                new Episode() { EpisodeId = 3},
                new Episode() { EpisodeId = 4},
                new Episode() { EpisodeId = 5},
                new Episode() { EpisodeId = 6},
                new Episode() { EpisodeId = 7}
            };

            p.AddEpisodes(eps);

            Assert.AreEqual(7, p.Episodes.Length);
            for (int i = 0; i < p.Episodes.Length; i++)
            {
                Assert.AreSame(p, eps[i].Project);
            }

            foreach (var character in p.TableRows.Keys)
            {
                Assert.AreSame(p.Episodes, p.TableRows[character].Episodes);
            }

        }
        [TestMethod]
        public void ProjectAddLinesTest()
        {
            Project p = new Project();
            Episode e1 = new Episode(p) { EpisodeId = 1 };
            Episode e2 = new Episode(p) { EpisodeId = 2 };
            Episode e3 = new Episode() { EpisodeId = 3 };

            Assert.AreEqual(2, p.Episodes.Length);
            Assert.IsTrue(p.Episodes.Contains(e1));
            Assert.IsTrue(p.Episodes.Contains(e2));
            Assert.IsFalse(p.Episodes.Contains(e3));
            Assert.AreSame(p, e1.Project);
            Assert.AreSame(p, e2.Project);

            e3.Project = p;
            Assert.IsTrue(p.Episodes.Contains(e3));

            Episode e4 = new Episode() { EpisodeId = 4 };
            p.AddEpisode(e4);

            Assert.IsTrue(p.Episodes.Contains(e4));
            Assert.AreEqual(4, p.Episodes.Length);
            Assert.AreSame(p, e4.Project);

            Character c1 = new Character() { CharacterId = 1 };
            Character c2 = new Character() { CharacterId = 2 };
            Character c3 = new Character() { CharacterId = 3 };
            Character c4 = new Character() { CharacterId = 4 };

            Line l1 = new Line() { LineId = 1 };
            Line l2 = new Line() { LineId = 2 };
            Line l3 = new Line() { LineId = 3 };
            Line l4 = new Line() { LineId = 4 };
            Line l5 = new Line() { LineId = 5 };
            Line l6 = new Line() { LineId = 6 };
            Line l7 = new Line() { LineId = 7 };
            Line l8 = new Line() { LineId = 8 };
            Line l9 = new Line() { LineId = 9 };
            Line l10 = new Line() { LineId = 10 };
            Line l11 = new Line() { LineId = 11 };

            l1.Episode = e1;

            //Assert.IsTrue(e1.Lines.Contains(l1));

            l1.Character = c1;

            Assert.IsTrue(e1.Characters.ContainsKey(c1));
            Assert.AreEqual(1, e1.Characters[c1].Lines);
            Assert.IsTrue(c1.Episodes.Contains(e1));
            Assert.IsTrue(c1.Lines.Contains(l1));
            Assert.IsTrue(e1.Lines.Contains(l1));
            //Assert.IsTrue(p.TableRowDictionary.ContainsKey(c1));
            //Assert.IsTrue(p.TableRowDictionary[c1].ContainsKey(e1));
            //Assert.AreEqual(1, p.TableRowDictionary[c1][e1].Lines);

            l2.Character = c2;
            l2.Episode = e1;

            Assert.IsTrue(e1.Characters.ContainsKey(c2));
            Assert.AreEqual(1, e1.Characters[c2].Lines);
            Assert.IsTrue(c2.Episodes.Contains(e1));
            Assert.IsTrue(c2.Lines.Contains(l2));
            Assert.IsTrue(e1.Lines.Contains(l2));
            Assert.AreEqual(2, e1.Lines.Length);
            //Assert.IsTrue(p.TableRowDictionary.ContainsKey(c2));
            //Assert.IsTrue(p.TableRowDictionary[c2].ContainsKey(e1));
            //Assert.AreEqual(1, p.TableRowDictionary[c1][e1].Lines);

            l3.Episode = e1;
            l3.Character = c1;

            Assert.AreEqual(2, e1.Characters[c1].Lines);



        }

        [TestMethod]
        public void ProjectTableRowsTest()
        {
            Project p = new Project() { ProjectId = 1 };

            Episode e1 = new Episode() { EpisodeId = 1 };
            Episode e2 = new Episode() { EpisodeId = 2 };

            Character c1 = new Character() { CharacterId = 1 };
            Character c2 = new Character() { CharacterId = 2 };

            Line l1 = new Line() { LineId = 1, Text = "een" };
            Line l2 = new Line() { LineId = 1, Text = "een Twee" };
            Line l3 = new Line() { LineId = 1, Text = "een twee drie" };
            Line l4 = new Line() { LineId = 1, Text = "een twee drie vier" };
            Line l5 = new Line() { LineId = 1, Text = "een twee drie vier vijf" };
            Line l6 = new Line() { LineId = 1, Text = "een twee drie vier vijf Zes" };
            Line l7 = new Line() { LineId = 1, Text = "een twee drie vier vijf zes Zeven" };
            Line l8 = new Line() { LineId = 1, Text = "een twee drie vier vijf Zes Zeven Acht" };
            Line l9 = new Line() { LineId = 1, Text = "een twee drie vier vijf Zes zeven acht negen" };


            e1.Project = p;

            Assert.IsTrue(p.Episodes.Contains(e1));
            Assert.AreSame(e1.Project, p);

            c1.Project = p;

            Assert.IsTrue(p.Characters.Contains(c1));
            //Assert.IsTrue(p.TableRowDictionary.ContainsKey(c1));
            //Assert.IsTrue(p.TableRows.Select(_ => _.Character).Contains(c1));
            //Assert.IsTrue(p.TableRows.Single(_ => _.Character == c1).Episodes.Contains(e1));
            //Assert.AreEqual(1, p.TableRows.Single(_ => _.Character == c1).LineCounts.Length);
            //Assert.AreEqual(0, p.TableRows.Single(_ => _.Character == c1).LineCounts[0].Lines);

            e2.Project = p;

            //Assert.IsTrue(p.TableRows.Single(_ => _.Character == c1).Episodes.Contains(e2));
            //Assert.AreEqual(2, p.TableRows.Single(_ => _.Character == c1).LineCounts.Length);
            //Assert.AreEqual(0, p.TableRows.Single(_ => _.Character == c1).LineCounts.Select(_=>_.Lines).Sum());

            l1.Character = c1;

            Assert.IsFalse(c1.Lines.Contains(l1));
            //Assert.AreEqual(0, p.TableRows.Single(_ => _.Character == c1).LineCounts.Select(_ => _.Lines).Sum());

            l1.Episode = e1;

            Assert.IsTrue(c1.Lines.Contains(l1));
            Assert.IsTrue(e1.Lines.Contains(l1));
            //Assert.IsTrue(p.TableRowDictionary.ContainsKey(c1));
            //Assert.IsTrue(p.TableRowDictionary[c1].ContainsKey(e1));
            //Assert.AreEqual(1, p.TableRowDictionary[c1][e1].Lines);
            //Assert.AreEqual(1, p.TableRows.Single(_ => _.Character == c1).LineCounts[0].Lines);

            l1.Character = c2;

            Assert.IsTrue(!c1.Lines.Contains(l1));
            Assert.IsTrue(e1.Lines.Contains(l1));
            Assert.IsTrue(!e1.Characters.ContainsKey(c1));
            Assert.IsTrue(c2.Lines.Contains(l1));
            Assert.IsTrue(!p.Characters.Contains(c1));
            //Assert.IsTrue(!p.TableRowDictionary.ContainsKey(c1));
            //Assert.IsTrue(!p.TableRows.Select(_ => _.Character).Contains(c1));
            Assert.IsTrue(p.Characters.Contains(c2));
            //Assert.IsTrue(p.TableRowDictionary.ContainsKey(c2));
            //Assert.IsTrue(p.TableRows.Select(_ => _.Character).Contains(c2));

            l2.Episode = e2;
            l2.Character = c2;

            Assert.IsTrue(e2.Characters.ContainsKey(c2));
            Assert.AreEqual(1, e2.Characters[c2].Lines);
            //Assert.AreEqual(2, p.TableRows.Single(_ => _.Character == c2).LineCounts.Length);
            //Assert.AreSame(p.TableRows.Single(_ => _.Character == c2).LineCounts[1], e2.Characters[c2]);
            //Assert.AreEqual(2, p.TableRows.Single(_ => _.Character == c2).LineCounts.Select(_ => _.Lines).Sum());
        }
    }
}
