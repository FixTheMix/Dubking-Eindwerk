using System;
using System.Linq;
using DubKing.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests
{
    [TestClass]
    public class CharacterVoiceTalentInteractionTest
    {
        Character _c;
        VoiceTalent _v;

        [TestInitialize]
        public void Initialize()
        {
            _c = new Character()
            {
                CharacterId = 1,
                Name = "Character Name"
            };
            _v = new VoiceTalent()
            {
                VoiceId = 1,
                FirstName = "Voice",
                SurName = "Talent"
            };
        }
        [TestMethod]
        public void SetCharacterVoiceShouldAddCharacterToVoice()
        {
            _c.VoiceTalent = _v;

            Assert.IsTrue(_v.Characters.Contains(_c));
        }

        [TestMethod]
        public void AddCharacterShouldSetVoiceTalent()
        {
            _v.AddCharacter(_c);

            Assert.AreSame(_v, _c.VoiceTalent);
        }

        [TestMethod]
        public void SetVoiceToNull()
        {
            _c.VoiceTalent = _v;
            _c.VoiceTalent = null;

            Assert.IsNull(_c.VoiceTalent);
            Assert.AreEqual(0, _v.Characters.Length);
        }

        [TestMethod]
        public void SetNewVoiceShouldRemoveCharacter()
        {
            _c.VoiceTalent = _v;
            _c.VoiceTalent = new VoiceTalent();

            Assert.IsFalse(_v.Characters.Contains(_c));
        }
    }
}
