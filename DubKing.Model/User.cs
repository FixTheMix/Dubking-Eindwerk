using DubKing.Model.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DubKing.Model
{
    
    [Serializable]
    public class User: INotifyChange, IEquatable<User>, INotifyDataErrorInfo, IDataErrorInfo

    {
        #region Fields
        const string ERROR_MIN_ONE_READ_WRITE = "min 1 user needs read/write access.";
        protected string _userName;
        protected string _password;
        protected ProjectModuleAccess _projectAccess;
        protected ModuleAccess _voiceLibraryAccess;
        protected ModuleAccess _scheduleAccess;
        protected SettingsModuleAccess _settingAccess;
        private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();


        public event EventHandler HasChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        #endregion
        #region Properties
        public virtual bool IsAdmin { get { return false; } }
        public virtual int Id { get; set; }
        public virtual string UserName
        {
            get { return _userName; }
            set
           {
                _userName = value.Trim();
                IsUnique = true;
                IsValidUserName();
                NotifyChange();
            }
        }
        public virtual string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value.Trim();
                IsValidPassword();
                NotifyChange();
            }
        }
        public virtual ProjectModuleAccess ProjectAccess
        {
            get
            {
                return _projectAccess;
            }
            set
            {
                _projectAccess = value;
                NotifyChange();
            }
        }
        public virtual  ModuleAccess VoiceLibraryAccess
        {
            get
            {
                return _voiceLibraryAccess;
            }
            set
            {
                _voiceLibraryAccess = value;
                NotifyChange();
            }
        }
        public virtual ModuleAccess ScheduleAccess
        {
            get
            {
                return _scheduleAccess;
            }
            set
            {
                _scheduleAccess = value;
                NotifyChange();
            }
        }
        public virtual SettingsModuleAccess SettingsAccess
        {
            get
            {
                return _settingAccess;
            }
            set
            {
                _settingAccess = value;
                RemoveSettingsAccessError(_settingAccess);
                NotifyChange();
            }
        }
        public bool IsUnique { get; set; } = true;

        public bool IsValid
        {
            get
            {
                Validate();
                if (_errorMessages.Count == 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool HasErrors
        {
            get
            {
                if (_errorMessages.Count != 0)
                {
                    return true;
                }
                return false;
            }
        }
        public IEnumerable GetErrors(string propertyName)
        {
            if (!_errorMessages.ContainsKey(propertyName))
            {
                return null;
            }
            return _errorMessages[propertyName];
        }
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                return (!_errorMessages.ContainsKey(columnName) ? null :
                    String.Join(Environment.NewLine, _errorMessages[columnName]));
            }
        }

        private void IsValidUserName()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                AddErrorMessage(nameof(UserName), Constants.ErrorMessages.Mandatory);
            }
            else
            {
                RemoveErrorMessage(nameof(UserName), Constants.ErrorMessages.Mandatory);
            }
            if (UserName.Length > 25)
            { 
                AddErrorMessage(nameof(UserName), Constants.ErrorMessages.LessThen25);
            }
            else
            {
                RemoveErrorMessage(nameof(UserName), Constants.ErrorMessages.LessThen25);
            }
            if (!IsUnique)
            {
                AddErrorMessage(nameof(UserName), Constants.ErrorMessages.Exists);
            }
            else
            {
                RemoveErrorMessage(nameof(UserName), Constants.ErrorMessages.Exists);
            }
        }
        private void IsValidPassword()
        {
            if (Password.Length > 25)
            {
                AddErrorMessage(nameof(Password), Constants.ErrorMessages.LessThen25);
            }
            else
            {
                RemoveErrorMessage(nameof(Password), Constants.ErrorMessages.LessThen25);
            }
        }
        private void RemoveErrorMessage(string propertyName, string errorMessage)
        {
            if (_errorMessages.ContainsKey(propertyName)&& _errorMessages[propertyName].Contains(errorMessage))
            {
                _errorMessages[propertyName].Remove(errorMessage);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
                if (_errorMessages[propertyName].Count == 0)
                {
                    _errorMessages.Remove(propertyName);
                }
            }
        }
        private void AddErrorMessage(string propertyName, string errorMessage)
        {
            if (!_errorMessages.ContainsKey(propertyName))
            {
                _errorMessages[propertyName] = new List<string>();
            }
            if (!_errorMessages[propertyName].Contains(errorMessage))
            {
                _errorMessages[propertyName].Add(errorMessage);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
        private void Validate()
        {
            IsValidUserName();
            IsValidPassword();
        }

        public void ValidateSettingsAccess(User[] users)
        {
            if (SettingsAccess == SettingsModuleAccess.NoAccess)
            {
                bool valid = (users.Where(u => u.SettingsAccess == SettingsModuleAccess.ReadWrite).Count() > 1);

                if (!valid)
                {
                    AddErrorMessage(nameof(SettingsAccess), ERROR_MIN_ONE_READ_WRITE);
                }
                else
                {
                    RemoveErrorMessage(nameof(SettingsAccess), ERROR_MIN_ONE_READ_WRITE);
                }
            }
            
        }
        private void RemoveSettingsAccessError(SettingsModuleAccess value)
        {
            if (value == SettingsModuleAccess.ReadWrite)
            {
                RemoveErrorMessage(nameof(SettingsAccess), ERROR_MIN_ONE_READ_WRITE);
            }
        }
        #endregion

        #region Constructors
        public User()
        {
            _userName = String.Empty;
            _password = String.Empty;
            _projectAccess = ProjectModuleAccess.ReadOnly;
            _voiceLibraryAccess = ModuleAccess.NoAccess;
            _scheduleAccess = ModuleAccess.NoAccess;
            _settingAccess = SettingsModuleAccess.NoAccess;
        }
        public User(int id, string n, string p):this()
        {
            Id = id;
            _userName = n;
            _password = p;
        }
        public User(int id, string u, string p, ProjectModuleAccess pm, ModuleAccess vl, ModuleAccess sch, SettingsModuleAccess set)
        {
            Id = id;
            _userName = u;
            _password = p;
            _projectAccess = pm;
            _voiceLibraryAccess = vl;
            _scheduleAccess = sch;
            _settingAccess = set;   
        }
        #endregion

        #region Methods
        private void NotifyChange()
        {
            HasChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool Equals(User other)
        {
            return this.Id == other.Id;
        }
        #endregion

        public static bool operator ==(User lUser, User rUser)
        {
            if (ReferenceEquals(lUser,null))
            {
                if (ReferenceEquals(rUser, null))
                {
                    return true;
                }
                return false;
            }
            if (!ReferenceEquals(rUser, null))
            {
                return lUser.Id.Equals(rUser.Id);
            }
            return false;
        }
        public static bool operator !=(User lUser, User rUser)
        {
            if (ReferenceEquals(lUser, null))
            {
                if (ReferenceEquals(rUser, null))
                {
                    return false;
                }
                return true;
            }
            if (!ReferenceEquals(rUser, null))
            {
                return !lUser.Id.Equals(rUser.Id);
            }
            return true;
        }
        public override string ToString()
        {
            return Id + " " + UserName;
        }

       
    }
}
