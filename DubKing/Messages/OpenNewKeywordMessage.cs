using DubKing.Model;
using DubKing.Model.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DubKing.Messages
{
    public class OpenNewKeywordMessage : IDataErrorInfo, INotifyPropertyChanged
    {
        private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();
        private string[] _existingKeywords = new string[0];
        private string _keyword;
        private string _comment;
        private readonly bool _isNewKeywordBox;

        
        public bool IsNewKeywordBox 
        {
            get { return _isNewKeywordBox; }
        }
        public string Keyword { get => _keyword; set { _keyword = value.ToTitleCase(); KeywordValidation(); } }
        public string Comment { get => _comment; set { _comment = value; CommentValidation(); } }
        public bool IsSaved { get; set; } = false;
        public string Error => throw new NotImplementedException();
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
        public string this[string columnName]
        {
            get
            {
                return (!_errorMessages.ContainsKey(columnName) ? null :
                    String.Join(Environment.NewLine, _errorMessages[columnName]));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public OpenNewKeywordMessage(string[] existingKeywords, bool isNewKeywordBox = true, string keyword = "", string comment = "")
        {
            _isNewKeywordBox = isNewKeywordBox;
            if (!_isNewKeywordBox)
            {
                _existingKeywords = existingKeywords.Where(_ => _ != keyword).ToArray();
            }
            else
            {
                _existingKeywords = existingKeywords;
            }
            _keyword = keyword;
            _comment = comment;
            
            
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
            if (_existingKeywords.Contains(_keyword))
            {
                AddErrorMessage(propertyName, Constants.ErrorMessages.Exists);
            }
            else
            {
                RemoveErrorMessage(propertyName, Constants.ErrorMessages.Exists);
            }
        }
        private void MandatoryValidation(string value, [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                AddErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
            }
            else
            {
                RemoveErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
            }
        }
        private void KeywordValidation([CallerMemberName] string propertyName = "")
        {
            LengthValidation(50, _keyword, propertyName);
            MandatoryValidation(_keyword, propertyName);
            DistinctValidation(propertyName);
        }
        private void CommentValidation([CallerMemberName] string propertyName = "")
        {
            LengthValidation(250, _comment, propertyName);
            MandatoryValidation(_comment, propertyName);
        }
        private void Validate()
        {
            KeywordValidation(nameof(Keyword));
            CommentValidation(nameof(Comment));
        }
    }
}
