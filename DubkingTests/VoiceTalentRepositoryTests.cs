//using System;
//using DubKing.Model;
//using DubKing.Repositories.Contracts;
//using DubKing.Repositories;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using FluentAssertions;
//using System.Net.Mail;

//namespace DubkingTests
//{
//    [TestClass]
//    public class VoiceTalentRepositoryTests
//    {
//        IRepository<VoiceTalent> _voiceTalentRepository;
//        int _id;
        
//        [TestMethod]
//        public void CreateTest()
//        {
//            VoiceTalent voiceTalent = new VoiceTalent()
//            {
//                FirstName = "Raf",
//                SurName = "Ferlin",
//                Birthday = new DateTime(1979, 11, 07),
//                Email = "Raf@fixthemix.be",
//                Expirience = "None",
//                Comment = "Nothing to say",
//                Rating = 5,
//                Gender = Gender.Male,
//                Adress = new Adress
//                {
//                    Street = "Groeneweg",
//                    Number = 19,
//                    City = "Wezemaal",
//                    PostalCode = 3111,
//                    Country = "Belgium"
//                }
//            };

//           var result = _voiceTalentRepository.Create(voiceTalent);

//            result.FirstName.Should().Be("Raf");
//            result.SurName.Should().Be("Ferlin");
           
//            result.Email.Should().Be("Raf@fixthemix.be");
//            result.Adress.Should().NotBe(0);
//            _id = result.Id;
//        }

//        [TestMethod]
//        public void GetAllTest()
//        {
            
//            var vts = _voiceTalentRepository.GetAll();

//            vts.Should().NotBeNull();
//            vts.Count.Should().Be(1);
//            vts[0].Adress.Street.Should().Be("Groeneweg");
//            vts[0].Id.Should().NotBe(0);
//        }

//        [TestMethod]
//        public void GetByIdTest()
//        {
            
//            var result = _voiceTalentRepository.GetById(2);

//            result.FirstName.Should().Be("Raf");
//            result.SurName.Should().Be("Ferlin");
//            result.Email.GetType().Should().Be(typeof(MailAddress));
//            result.Email.Should().Be("Raf@fixthemix.be");
//            result.Id.Should().NotBe(0);
//            result.Adress.Street.Should().Be("Groeneweg");
//            result.Adress.AdressId.Should().NotBe(0);
//        }
//        [TestMethod]
//        public void DeleteTest()
//        {
//            _voiceTalentRepository.Delete(_voiceTalentRepository.GetById(2));

//            var result = _voiceTalentRepository.GetById(1);

//            result.Should().Be(null);
//        }
//        [TestMethod]
//        public void UpdateTest()
//        {
//            CreateTest();
//            var vt = _voiceTalentRepository.GetById(_id);

//            vt.FirstName = "Wendy";
//            vt.SurName = "Creemers";
//            vt.Birthday = new DateTime(1977, 09, 03);
//            vt.Email = new MailAddress("CreemersWendy@hotmail.com");
//            vt.Adress = new Adress
//            {
//                Id = vt.Adress.Id,
//                Street = "Strijdersstraat",
//                Number = 61,
//                PoBox = "1B",
//                City = "Leuven",
//                PostalCode = 3000,
//                Country = "Nederland"
//            };
//            vt.Expirience = "Helemaal Geen";
//            vt.Comment = "Geen commantaar";
//            vt.Rating = 1;
//            vt.Gender = Gender.Female;

//            _voiceTalentRepository.Update(vt);

//            var voiceRepository = new VoiceTalentRepository();

//            var newVt = voiceRepository.GetById(vt.Id);

//            newVt.FirstName.Should().Be("Wendy");
//            newVt.SurName.Should().Be("Creemers");
//            newVt.Birthday.Value.Year.Should().Be(1977);
//            newVt.Birthday.Value.Month.Should().Be(9);
//            newVt.Birthday.Value.Day.Should().Be(3);
//            newVt.Email.Address.Should().Be("CreemersWendy@hotmail.com");
//            newVt.Adress.Street.Should().Be("Strijdersstraat");
//            newVt.Adress.Number.Should().Be(61);
//            newVt.Adress.PoBox.Should().Be("1B");
//            newVt.Adress.City.Should().Be("Leuven");
//            newVt.Adress.PostalCode.Should().Be(3000);
//            newVt.Adress.Country.Should().Be("Nederland");
//            newVt.Expirience.Should().Be("Helemaal Geen");
//            newVt.Comment.Should().Be("Geen commantaar");
//            newVt.Rating.Should().Be(1);
//            newVt.Gender.Should().Be(Gender.Female);

//            _voiceTalentRepository.Delete(vt);

//        }
//        public VoiceTalentRepositoryTests()
//        {
//            _voiceTalentRepository = new VoiceTalentRepository();
//        }
//    }
//}
