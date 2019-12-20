using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DubKing.Model
{
    public class ActiveActor : INotifyPropertyChanged
    {
        private IEnumerable<ActiveProject> _projects;

        public int VoiceId { get; set; }
        public string Name { get; set; }
        public SimpleLineCount LineCount { get; set; }
        public IEnumerable<ActiveProject> Projects { get => _projects; set { _projects = value; RaisePropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
