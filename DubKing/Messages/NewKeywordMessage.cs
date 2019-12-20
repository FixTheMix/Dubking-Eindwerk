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
    public class NewKeywordMessage : IDataErrorInfo, INotifyPropertyChanged
    {
        private string _keyword;
        private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();

        public bool IsSaved { get; set; }
        public string Keyword
        {
            get => _keyword; set
            {
                if (_keyword != value)
                {
                    _keyword = value;
                    KeywordValidation();
                } } }
        public IEnumerable<VLKeyword> Keywords { get; private set; }
        public string Error => throw new NotImplementedException();
        public bool IsValid
        {
            get
            {
                KeywordValidation();
                if (_errorMessages.Count == 0)
                {
                    return true;
                }
                return false;

            }
        }
        public string this[string columnName]
        {
            get
            {
                return (!_errorMessages.ContainsKey(columnName) ? null :
                    String.Join(Environment.NewLine, _errorMessages[columnName]));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public NewKeywordMessage(IEnumerable<VLKeyword> keywords)
        {
            Keywords = keywords;
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
        private void LengthValidation(int maxLenght, string value, [CallerMemberName]string propertyName = "")
        {
            if (value == null)
            {
                return;
            }
            string error = Constants.ErrorMessages.LessthenVariable(maxLenght);
            if (value.Length > maxLenght)
            {
                AddErrorMessage(propertyName, error);
            }
            else
            {
                RemoveErrorMessage(propertyName, error);
            }
        }
        private void DistinctValidation([CallerMemberName] string propertyName = "")
        {
            if (Keywords.Select(_ => _.KeyWord).Contains(_keyword))
            {
                AddErrorMessage(propertyName, Constants.ErrorMessages.Exists);
            }
            else
            {
                RemoveErrorMessage(propertyName, Constants.ErrorMessages.Exists);
            }
        }
        private void MandatoryValidation([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrEmpty(_keyword))
            {
                AddErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
            }
            else
            {
                RemoveErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
            }
        }
        private void KeywordValidation()
        {
            LengthValidation(25, _keyword, nameof(Keyword));
            MandatoryValidation(nameof(Keyword));
            DistinctValidation(nameof(Keyword));
        }

    }
}
