using System;
using System.Collections.Generic;
using System.Windows.Input;
using DubKing.Helpers;
using DubKing.Model.Contracts;
using DubKing.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace DubKing.ViewModel
{
    //public delegate void ShowDetailsEvent();
    public class BarViewModel<T> : ViewModelBase where T : INotifyChange
    {
        #region Fields
        public static event EventHandler OpenObjectChanged;
        static BarViewModel<T> _openObject;
        T _object;
        List<UserCheck> _allUsers;
        ICommand _showDetailsCommand;
        ICommand _DoubleClickCommand;
        bool _showDetails;
        private bool _isReadOnly;

        public event EventHandler ObjectChanged;
        #endregion

        #region Properties
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { Set(ref _isReadOnly, value); }
        }
        static public BarViewModel<T> OpenObject
        {
            get { return _openObject; }
            set
            {
                _openObject = value;
                OpenObjectChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        public T Object
        {
            get { return _object; }
            set
            {
                Set(ref _object, value);
            }
        }
        public bool ShowDetails
        {
            get { return _showDetails; }
            set { Set(ref _showDetails, value); }
        }
        public ICommand ShowDetailsCommand
        {
            get { return _showDetailsCommand ?? (new RelayCommand(OnShowDetails)); }
        }
        public ICommand DoubleClickCommand
        {
            get { return _DoubleClickCommand ?? (new RelayCommand(OnDoubleClick)); }
        }
        #endregion

        private void OnShowDetails()
        {
            OpenObject = this;
        }
        private void OnDoubleClick()
        {
            Messenger.Default.Send(new MessageOpen<T>(_object));
        }
        private void SetShowDetails(object obj, EventArgs e)
        {
            if (_openObject == this)
            {
                ShowDetails = !ShowDetails;
            }
            else
            {
                ShowDetails = false;
            }
        }
        private void NotifyObjectChanged(object obj, EventArgs e)
        {
            ObjectChanged?.Invoke(obj, e);
        }
        #region constructor
        public BarViewModel(T obj)
        {
            _object = obj;
            OpenObjectChanged += SetShowDetails;
            _object.HasChanged += NotifyObjectChanged;
        }
        #endregion

    }
}
