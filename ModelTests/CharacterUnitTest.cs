using System;
using DubKing.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class CharacterUnitTest
    {
        [TestMethod]
        public void EqualityTest()
        {
            Character c1 = new Character() { CharacterId = 1 };
            Character c2 = new Character() { CharacterId = 2 };

            Assert.IsFalse(c1 == c2);
            Assert.IsTrue(c1 != c2);

            c2.CharacterId = 1;

            Assert.IsTrue(c1 == c2);
            Assert.IsFalse(c1 != c2);
        }
        [TestMethod]
        public void LinesNotRecordedShouldSetStatusIsRecordedToFalse()
        {
            var c = CreateCharacterWithLines();

            Assert.IsFalse(c.IsRecorded);
        }
        [TestMethod]
        public void LinesHasOneNotRecLineShouldSetStatusToFalse()
        {
            var c = CreateCharacterWithLines();
            c.Lines[0].IsRecorded = true;

            Assert.IsFalse(c.IsRecorded);
        }
        [TestMethod]
        public void AllLinesRecordedShouldReturnStatusTrue()
        {
            var c = CreateCharacterWithLines();
            SetAllLinesToRec(c);

            Assert.IsTrue(c.IsRecorded);
        }
        [TestMethod]
        public void AddingNotRecordedLineShouldSetRecStatusToFalse()
        {
            var c = CreateCharacterWithLines();
            var l = new Line();
            SetAllLinesToRec(c);
            c.AddLine(l);

            Assert.IsFalse(c.IsRecorded);

        }
        [TestMethod]
        public void RemovingNotRecLineShoulsReturnRecStatusTrue()
        {
            var c = CreateCharacterWithLines();
            var l = new Line();
            SetAllLinesToRec(c);
            c.AddLine(l);
            c.RemoveLine(l);

            Assert.IsTrue(c.IsRecorded);
        }


        private Character CreateCharacterWithLines()
        {
            Character c1 = new Character() { CharacterId = 1 };
            c1.AddLine(new Line());
            c1.AddLine(new Line());
            return c1;
        }
        private static void SetAllLinesToRec(Character c)
        {
            foreach (var l in c.Lines)
            {
                l.IsRecorded = true;
            }
        }

    }
}
