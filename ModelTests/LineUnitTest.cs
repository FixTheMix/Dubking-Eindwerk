using System;
using System.Linq;
using DubKing.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class LineUnitTest
    {
        [TestMethod]
        public void EqualityTest()
        {
            Line l1 = new Line() { LineId = 1 };
            Line l2 = new Line() { LineId = 1 };

            Assert.IsTrue(l1 == l2);
        }
        [TestMethod]
        public void NotEqualityTest()
        {
            Line l1 = new Line() { LineId = 1 };
            Line l2 = new Line() { LineId = 2 };

            Assert.IsTrue(l1 != l2);
        }
        [TestMethod]
        public void TestSetCharacter()
        {
            Line l = new Line() { Episode = new Episode() {EpisodeId =5 } };
            Character c = new Character() { CharacterId = 1, Name = "Naam1" };
            Character c2 = new Character() { CharacterId = 1, Name = "Naam1" };

            l.Character = c;

            Assert.AreSame(l.Character, c);
            Assert.AreEqual(1, c.Lines.Length);
            Assert.AreSame(l, c.Lines[0]);

            l.Character = c2;
            l.Character.Name = "Naam2";

            Assert.AreEqual(l.Character.Name, "Naam2");
            Assert.AreEqual(1, c.Lines.Length);
            Assert.AreSame(l.Character, c);
        }[TestMethod]
        public void TestReplaceCharacter()
        {
            Line l = new Line();
            Episode e = new Episode();
            Character c = new Character() { CharacterId = 1, Name = "Naam1" };
            Character c2 = new Character() { CharacterId = 2, Name = "Naam2" };

            l.Episode = e;
            l.Character = c;

            Assert.AreSame(l.Character, c);
            Assert.AreEqual(1, c.Lines.Length);
            Assert.AreSame(l, c.Lines[0]);

            l.Character = c2;

            Assert.AreEqual(l.Character.Name, "Naam2");
            Assert.AreEqual(1, c2.Lines.Length);
            Assert.AreEqual(0, c.Lines.Length);
            Assert.IsFalse(e.Characters.Keys.Contains(c));
            Assert.IsTrue(e.Characters.Keys.Contains(c2));
            Assert.AreEqual(1, e.Characters.Keys.Count);
            Assert.IsFalse(c.Episodes.Contains(e));
            Assert.AreEqual(0, c.Episodes.Length);
            Assert.IsTrue(c2.Episodes.Contains(e));
            Assert.AreEqual(c2.Episodes.Length, 1);
        }
        [TestMethod]
        public void TestSetEpisode()
        {
            Line l = new Line();
            Episode e = new Episode();
            Character c = new Character() { CharacterId = 53 };

            l.Episode = e;
            l.Character = c;

            Assert.AreEqual(1, e.Lines.Length);
            Assert.AreSame(l, e.Lines[0]);

            l.Episode = e;

            Assert.AreEqual(1, e.Lines.Length);

        }
        [TestMethod]
        public void SetEpisodeNullTest()
        {
            Line l = new Line();
            Episode e = new Episode();

            l.Episode = e;

            l.Episode = null;

            Assert.AreSame(l.Episode, e);
        }
        [TestMethod]
        public void TestCharaterEpisode()
        {
            Line l = new Line();
            Episode e = new Episode();
            Character c = new Character();

            l.Character = c;
            l.Episode = e;

            Assert.IsTrue(c.Episodes.Contains(e));
            Assert.IsTrue(e.Characters.ContainsKey(c));
            Assert.IsTrue(c.Lines.Contains(l));
            Assert.IsTrue(e.Lines.Contains(l));
        }
        [TestMethod]
        public void TestEpisodeCharacter()
        {
            Line l = new Line();
            Episode e = new Episode();
            Character c = new Character();

            l.Episode = e;
            l.Character = c;

            Assert.IsTrue(c.Episodes.Contains(e));
            Assert.IsTrue(e.Characters.ContainsKey(c));
            Assert.IsTrue(c.Lines.Contains(l));
            Assert.IsTrue(e.Lines.Contains(l));
        }
    }

}
