using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    abstract public class ValidationBase : IDataErrorInfo
    {
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
        protected Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();
        public string Error => throw new NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                return (!_errorMessages.ContainsKey(columnName) ? null :
                    String.Join(Environment.NewLine, _errorMessages[columnName]));
            }
        }
        protected void AddErrorMessage(string propertyName, string errorMessage)
        {
            if (!_errorMessages.ContainsKey(propertyName))
            {
                _errorMessages[propertyName] = new List<string>();
            }
            if (!_errorMessages[propertyName].Contains(errorMessage))
            {
                _errorMessages[propertyName].Add(errorMessage);
            }
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void RemoveErrorMessage(string propertyName, string errorMessage)
        {
            if (_errorMessages.ContainsKey(propertyName) && _errorMessages[propertyName].Contains(errorMessage))
            {
                _errorMessages[propertyName].Remove(errorMessage);
                if (_errorMessages[propertyName].Count == 0)
                {
                    _errorMessages.Remove(propertyName);
                }
            }
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void MandatoryValidation(string propertyName, string value)
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
        protected void LengthValidation(string propertyName, int maxLenght, string value)
        {
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
    }
}
