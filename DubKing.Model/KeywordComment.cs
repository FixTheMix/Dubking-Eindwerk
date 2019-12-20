using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class KeywordComment : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _glossaryId;
        private string _keyword;
        private string _comment;
        private Project _project;
        private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();

        #region Properties
        public int GlossaryId
        {
            get => _glossaryId;
            set
            {
                _glossaryId = value;
                RaisePropertyChanged();
            }
        }
        public string Keyword
        {
            get => _keyword;
            set
            {
                _keyword = value;
                ValidateKeyword();
            }
        }
        public string Comment
        {
            get => _comment; set
            {
                _comment = value;
                ValidateComment();
            }
        }
        public Project Project
        {
            get => _project;
            set
            {
                SetProject(value);
                RaisePropertyChanged();
            }
        }
        public bool IsValid
        {
            get
            {
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
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private void SetProject(Project p)
        {
            if (p == null) return;
            if (p == _project) return;
            _project = p;
            _project.AddKeywordComment(this);
        }
        private void RaisePropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private void ValidateKeyword([CallerMemberName]string propName = "")
        {
            ValidateLength(_keyword, 50, propName);
            ValidateMandatory(_keyword, propName);
            ValidateUniqueKeyWord(_keyword, propName);
        }
        private void ValidateComment([CallerMemberName]string propName = "")
        {
            ValidateLength(_comment, 250, propName);
            ValidateMandatory(_comment, propName);
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
        private void ValidateLength(string value, int length, [CallerMemberName]string propName = "")
        {
            if (value == null) return;
            if (value.Length > length)
            {
                AddErrorMessage(propName, Constants.ErrorMessages.LessthenVariable(length));
            }
            else
            {
                RemoveErrorMessage(propName, Constants.ErrorMessages.LessthenVariable(length));
            }
        }
        private void ValidateMandatory(string value, [CallerMemberName]string propName = "")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                AddErrorMessage(propName, Constants.ErrorMessages.Mandatory);
            }
            else
            {
                RemoveErrorMessage(propName, Constants.ErrorMessages.Mandatory);
            }
        }
        private void ValidateUniqueKeyWord(string value, [CallerMemberName]string propName = "")
        {
            if (_project != null)
            {
                if (_project.HasKeywordComment(value))
                {
                    AddErrorMessage(propName, Constants.ErrorMessages.Exists);
                }
                else
                {
                    RemoveErrorMessage(propName, Constants.ErrorMessages.Exists);
                }
            }
        }
    }
}
