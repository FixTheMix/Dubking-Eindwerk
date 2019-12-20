using DubKing.Model.Contracts;
using DubKing.Model.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DubKing.Model
{
    public enum Gender { Male, Female }
    public class VoiceTalent : INotifyChange, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields
        string _firstName;
        string _surName;
        DateTime? _birthday;
        string _email;
        Adress _adress;
        string _language;
        List<TelephoneNumber> _telephoneNumbers;
        string _expirience;
        string _comment;
        int _rating;
        Gender _gender;
        string _picture;
        List<Session> _sessions;
        List<SessionLog> _sessionLogs;
        Character[] _characters = new Character[0];
        List<VLKeyword> _keywords;
        private string _homeNumber;
        private string _workNumber;
        private string _mobileNumber;
        private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();
        private bool _isActive;
        #endregion

        #region Events
        public event EventHandler HasChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public Project[] Projects { get => _characters.Select(_=>_.Project).Distinct().ToArray(); }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        public int VoiceId { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value.Trim();
                ValidateFirstName();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(FullName));
                NotifyChange();
            }
        }
        public string SurName
        {
            get { return _surName; }
            set
            {
                _surName = value.Trim();
                ValidateSurName();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(FullName));
                NotifyChange();
            }
        }
        public string FullName { get { return FirstName + " " + SurName; } }
        public DateTime? Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                NotifyChange();
                RaisePropertyChanged();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Age"));
            }
        }
        public int Age
        {
            get
            {
                if (Birthday != null)
                {
                    return (DateTime.Now.Year - Birthday.Value.Year);
                }
                return 0;
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                ValidateEmail();
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public Adress Adress
        {
            get { return _adress; }
            set
            {
                if (value != null)
                {
                    _adress = value;
                    SubscribeToChangeEvent(_adress);
                    NotifyChange();
                }
            }
        }
        public int LanguageId { get; set; }
        public string Language
        {
            get { return _language; }
            set
            {
                if (value != null)
                {
                    _language = value.Trim();
                    RaisePropertyChanged();
                    LengthValidation(nameof(Language), 25, _language);
                    NotifyChange();
                }
            }
        }
        public string HomeNumber
        {
            get { return _homeNumber; }
            set
            {
                _homeNumber = value;
                LengthValidation(15, _homeNumber);
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public string WorkNumber
        {
            get { return _workNumber; }
            set
            {
                _workNumber = value;
                LengthValidation(15, _workNumber);
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set
            {
                _mobileNumber = value;
                LengthValidation(15, _mobileNumber);
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public string Expirience
        {
            get
            {
                return _expirience;
            }
            set
            {
                _expirience = value;
                ValidateExpirience();
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                ValidateComment();
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public int Rating {
            get
            {
                return _rating;
            }
            set
            {
                _rating = value;
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public Gender Gender {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public string Picture {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Picture"));
                NotifyChange();
            }
        }
        public List<Session> Sessions {
            get
            {
                return _sessions;
            }
            set
            {
                _sessions = value;
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public List<SessionLog> SessionLogs {
            get
            {
                return _sessionLogs;
            }
            set
            {
                _sessionLogs = value;
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public Character[] Characters {
            get
            {
                return _characters;
            }
        }
        public List<VLKeyword> Keywords
        {
            get
            {
                return _keywords;
            }
            set
            {
                _keywords = value;
                RaisePropertyChanged();
                NotifyChange();
            }
        }
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
        public string Error => throw new NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                return (!_errorMessages.ContainsKey(columnName) ? null :
                    String.Join(Environment.NewLine, _errorMessages[columnName]));
            }
        }

        public void AddCharacter(Character c)
        {
            if (!Characters.Contains(c))
            {
                SetCharacterVoiceTalentToThis(c);
                var temp = _characters.ToList();
                temp.Add(c);
                _characters = temp.ToArray();
                RegisterCharacter(c);
                RaiseEventsForCharactersProperty();
            }
        }
        private void RegisterCharacter(Character c)
        {
            SetIsActive(null, c.IsRecorded);
            c.IsRectordedChanged += SetIsActive;
        }
        private void UnRegisterCharacter(Character c)
        {
            c.IsRectordedChanged -= SetIsActive;
            SetIsActive();
        }
        private void SetIsActive(object sender = null, bool recStatus = false)
        {
            if (!recStatus)
            {
                _isActive = true;
            }
            else
            {
                _isActive = _characters.Where(_ => !_.IsRecorded).Count() > 0;
            }
            NotifyChange();
        }
        private void RaiseEventsForCharactersProperty()
        {
            RaisePropertyChanged(nameof(Characters));
            NotifyChange();
        }
        private void SetCharacterVoiceTalentToThis(Character c)
        {
            c.VoiceTalent = this;
        }
        public void RemoveCharacter(Character c)
        {
            var temp = _characters.ToList();
            temp.Remove(c);
            _characters = temp.ToArray();
            UnRegisterCharacter(c);
            RaiseEventsForCharactersProperty();
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
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void RemoveErrorMessage(string propertyName, string errorMessage)
        {
            if (_errorMessages.ContainsKey(propertyName) && _errorMessages[propertyName].Contains(errorMessage))
            {
                _errorMessages[propertyName].Remove(errorMessage);
                if (_errorMessages[propertyName].Count == 0)
                {
                    _errorMessages.Remove(propertyName);
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Validate()
        {
            ValidateFirstName();
            ValidateSurName();
            ValidateComment();
            ValidateEmail();
            ValidateExpirience();
        }
        private void ValidateFirstName()
        {
            MandatoryValidation(nameof(FirstName), FirstName);
            LengthValidation(nameof(FirstName), 50, FirstName);
        }
        private void ValidateSurName()
        {
            MandatoryValidation(nameof(SurName), SurName);
            LengthValidation(nameof(SurName), 50, SurName);
        }
        private void ValidateComment()
        {
            LengthValidation(nameof(Comment), 600, Comment);
        }
        private void ValidateExpirience()
        {
            LengthValidation(nameof(Expirience), 600, Expirience);
        }
        private void ValidateEmail()
        {
            try
            {
                if (!string.IsNullOrEmpty(_email))
                {
                    var email = new MailAddress(_email);
                }
                RemoveErrorMessage(nameof(Email), Constants.ErrorMessages.InValidEMail);
            }
            catch (FormatException)
            {
                AddErrorMessage(nameof(Email), Constants.ErrorMessages.InValidEMail);
            }
        }

        private void MandatoryValidation(string propertyName, string value)
        {
            bool notValid = string.IsNullOrEmpty(value);
            if (notValid)
                {
                    AddErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
                }
                else
                {
                    RemoveErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
                }
        }
        private void LengthValidation(int maxLenght, string value, [CallerMemberName]string propertyName = "")
        {
            LengthValidation(propertyName, maxLenght, value);
        }
        private void LengthValidation(string propertyName, int maxLenght, string value)
        {
            if (value == null)
            {
                return;
            }
            string error = Constants.ErrorMessages.LessthenVariable(maxLenght);
            if (value.Length > maxLenght)
            {
                AddErrorMessage(propertyName,error);
            }
            else
            {
                RemoveErrorMessage(propertyName, error);
            }
        }

        #endregion


        #region Constructors
        public VoiceTalent()
        {
            _telephoneNumbers = new List<TelephoneNumber>();
            _gender = Gender.Male;
            //SubscribeToChangeEvent(_adress);
            _sessions = new List<Session>();
            _sessionLogs = new List<SessionLog>();
            _keywords = new List<VLKeyword>();
            _adress = new Adress();
        }
        #endregion
        #region Methodes
        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void NotifyChange()
        {
            HasChanged?.Invoke(this, EventArgs.Empty);
        }
        private void SubscribeToChangeEvent(INotifyChange item)
        {
            item.HasChanged += FireNotifyChange;
        }

        private void FireNotifyChange(object obj, EventArgs e)
        {
            NotifyChange();
        }
        public override string ToString()
        {
            string result;
            if (Birthday != null)
            {
                result = $"Name: {FirstName} {SurName} \n" +
                $"Birthday: {Birthday.Value.ToShortDateString()} Email: {Email}\n" +
                $"Language: {Language} Expirience: {Expirience} Comment: {Comment}\n";
            }
            else
            {
                result = $"Name: {FirstName} {SurName} \n" +
                $"Birthday: N/A. Email: {Email}\n" +
                $"Language: {Language} Expirience: {Expirience} Comment: {Comment}\n";
            }
             
            return result;
        }
        private MailAddress CreateMail(string mail)
        {
            return new MailAddress(mail);
        }
        #endregion

    }
}
