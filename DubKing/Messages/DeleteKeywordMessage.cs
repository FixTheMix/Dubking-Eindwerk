using DubKing.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class DeleteKeywordMessage : INotifyPropertyChanged
    {
        private VLKeyword _selectedKeyword;

        public IEnumerable<VLKeyword> Keywords { get; private set; }
        public VLKeyword SelectedKeyword
        {
            get { return _selectedKeyword; }
            set
            {
                _selectedKeyword = value;
                RaisePropertyChanged();
            }
        }
        public bool IsCanceled { get; set; } = true;
        public DeleteKeywordMessage(IEnumerable<VLKeyword> keywords)
        {
            Keywords = keywords;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
