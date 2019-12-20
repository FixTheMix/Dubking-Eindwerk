using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DubKing.Model;
using DubKing.Repositories.Contracts;
using DubKing.Services.Contracts;

namespace DubKing.Services
{
    public class VoiceTalentService : IVoiceTalentService
    {
        private readonly IRepository<VoiceTalent> _voiceTalentRepository;
        private readonly IRepository<VLKeyword> _keywordRepository;

        private List<VoiceTalent> _voiceTalentsCache = new List<VoiceTalent>();
        private DateTime _lastUpdate;
        
        public VoiceTalent Create(VoiceTalent voiceTalent)
        {
            return _voiceTalentRepository.Create(voiceTalent);
        }

        public IList<VoiceTalent> GetAll()
        {
            if(DateTime.Now.Subtract(_lastUpdate).Minutes > 15 || _voiceTalentsCache.Count == 0)
            {
                _voiceTalentsCache = _voiceTalentRepository.GetAll();
                _lastUpdate = DateTime.Now;
            }
            return _voiceTalentsCache;
        }

        public VoiceTalent GetById(int id)
        {
            return _voiceTalentRepository.GetById(id);
        }

        public void Update(VoiceTalent voiceTalent)
        {
            _voiceTalentRepository.Update(voiceTalent);
        }

        public void Delete(VoiceTalent voiceTalent)
        {
            _voiceTalentRepository.Delete(voiceTalent);
        }

        public string CopyVoicePic(string path, VoiceTalent vt)
        {
            string destination = $"{ConfigurationManager.AppSettings["PicsLocation"]}\\{vt.VoiceId.ToString()}_{vt.SurName}_{vt.FirstName}{Path.GetExtension(path)}";

            File.Copy(path, destination, true);

            return destination;
        }

        public void SetPicture(string path, VoiceTalent vt)
        {
            if (path != null)
            {
                vt.Picture = CopyVoicePic(path, vt);
            }
        }

        public IList<VLKeyword> GetVLKeywords()
        {
            return _keywordRepository.GetAll().OrderBy(_ => _.KeyWord).ToList(); ;
        }

        public VLKeyword CreateKeyword(string keyword)
        {
            return _keywordRepository.Create(new VLKeyword(keyword));
        }

        public void DeleteVlKeyword(VLKeyword keyword)
        {
            _keywordRepository.Delete(keyword);
        }

        public IList<VoiceTalent> GetActiveActors()
        {
            var result = GetAll().Where(_ => _.IsActive == true).ToList();
            return result;
        }

        public VoiceTalentService(IRepository<VoiceTalent> voiceTalentRepository, IRepository<VLKeyword> keywordRepository)
        {
            _voiceTalentRepository = voiceTalentRepository;
            _keywordRepository = keywordRepository;
        }
    }
}
