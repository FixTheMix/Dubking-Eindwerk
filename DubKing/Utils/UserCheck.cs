using DubKing.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Utils
{
    public delegate void AddUser(User user);
    public class UserCheck : INotifyPropertyChanged
    {
        private bool _selected;
        public User User { get; set; }
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected == value) return;
                _selected = value;
                UserAdded?.Invoke(this, EventArgs.Empty);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }
        public bool CanChange { get; set; }

        public void SetSelected(bool isSelected, bool raiseEvent = false)
        {
            _selected = isSelected;
            if (raiseEvent)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }
        public UserCheck(User user)
        {
            User = user;
            CanChange = true;
        }
        public event EventHandler UserAdded;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
