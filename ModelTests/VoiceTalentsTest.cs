using System;
using DubKing.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class VoiceTalentsTest
    {
        VoiceTalent _vt;
        [TestInitialize]
        public void Init()
        {
            _vt = new VoiceTalent();
        }
        [TestMethod]
        public void WhenNoCharacterIsactiveShouldReturnFalse()
        {
            Assert.IsFalse(_vt.IsActive);
        }
        [TestMethod]
        public void WhenCharacterIsNotRecIsActiveShouldReturnTrue()
        {
            var c = new Character();
            c.AddLine(new Line());
            c.AddLine(new Line());
            _vt.AddCharacter(c);

            Assert.IsTrue(_vt.IsActive);
        }
        [TestMethod]
        public void WhenCharacterIsRecIsActiveShouldReturnFalse()
        {
            _vt.VoiceId = 1;
            var c = new Character() { CharacterId = 1 };
            c.AddLine(new Line());
            c.AddLine(new Line());
            foreach (var l in c.Lines)
            {
                l.IsRecorded = true;
            }
            _vt.AddCharacter(c);

            Assert.IsFalse(_vt.IsActive);
        }
        [TestMethod]
        public void WhenLineStateChangesIsActiveShouldChange()
        {
            _vt.VoiceId = 1;
            var c = new Character() { CharacterId = 1 };
            c.AddLine(new Line());
            c.AddLine(new Line());
            _vt.AddCharacter(c);

            Assert.IsTrue(_vt.IsActive, "IsActive Returns False When it Should be True Before Changing Line Status");

            foreach (var l in c.Lines)
            {
                l.IsRecorded = true;
            }

            Assert.IsFalse(_vt.IsActive, "IsActive Returns True when ik should be False After Setting Line Status To Rec");

            foreach (var l in c.Lines)
            {
                l.IsRecorded = false;
            }

            Assert.IsTrue(_vt.IsActive, "IsActive Returns False when ik should be True After Changing LineStatus To NotRec");
        }
    }
}
