using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services.Contracts
{
    public interface IVoiceTalentService
    {
        VoiceTalent Create(VoiceTalent voiceTalent);
        IList<VoiceTalent> GetAll();
        VoiceTalent GetById(int id);
        void Update(VoiceTalent voiceTalent);
        void Delete(VoiceTalent voiceTalent);
        string CopyVoicePic(string path, VoiceTalent vt);
        void SetPicture(string path, VoiceTalent vt);
        IList<VLKeyword> GetVLKeywords();
        VLKeyword CreateKeyword(string keyword);
        void DeleteVlKeyword(VLKeyword keyword);
        IList<VoiceTalent> GetActiveActors();
    }
}
