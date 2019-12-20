using DubKing.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.ViewModel.ActiveActors
{
    public class VoiceTalentViewModel:ViewModelBase
    {
        private VoiceTalent _voiceTalent;
        private LineCount _lineCount;

        public VoiceTalent VoiceTalent
        {
            get { return _voiceTalent; }
            set { Set(ref _voiceTalent, value); }
        }

        public LineCount LineCount
        {
            get { return _lineCount; }
            set { Set(ref _lineCount, value); }
        }

        public VoiceTalentViewModel(VoiceTalent voiceTalent)
        {
            _voiceTalent = voiceTalent;
        }

    }
}
