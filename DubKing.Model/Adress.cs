using DubKing.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DubKing.Model.Extentions;

namespace DubKing.Model
{
    public class Adress:ValidationBase, INotifyChange
    {
        #region Fields
        string _street;
        int _number;
        string _poBox;
        string _city;
        int _postalCode;
        string _country;
        #endregion
        #region Properties
        public int AdressId { get; set; }
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                ValidateStreet();
                NotifyChange();
            }
        }
        public int Number {
            get { return _number; }
            set
            {
                _number = value;
                NotifyChange();
            }
        }
        public string PoBox {
            get { return _poBox; }
            set
            {
                _poBox = value;
                ValidatePoBox();
                NotifyChange();
            }
        }
        public string City {
            get { return _city; }
            set
            {
                _city = value;
                ValidateCity();
                NotifyChange();
            }
        }
        public int PostalCode {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                NotifyChange();
            }
        }
        public string Country {
            get { return _country; }
            set
            {
                _country = value;
                ValidateCountry();
                NotifyChange();
            }
        }
        #endregion

        private void ValidateStreet()
        {
            LengthValidation(nameof(Street), 120, Street);
        }
        private void ValidatePoBox()
        {
            LengthValidation(nameof(PoBox), 5, PoBox);
        }
        private void ValidateCity()
        {
            LengthValidation(nameof(City), 25, City);
        }
        private void ValidateCountry()
        {
            LengthValidation(nameof(Country), 50, Country);
        }
        

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the adress class.
        /// </summary>
        public Adress()
        {
           
        }
        /// <summary>
        /// Initializes a new instance of the adress class.
        /// </summary>
        /// <param name="s">street</param>
        /// <param name="n">Number</param>
        /// <param name="b">PoBox</param>
        /// <param name="ci">City</param>
        /// <param name="p">PostalCode</param>
        /// <param name="co">Country</param>
        public Adress(string s, int n, string ci, int p, string co, string b = "n.a.")
        {
            Street = s;
            Number = n;
            PoBox = b;
            City = ci;
            PostalCode = p;
            Country = co;
        }

        
        #endregion

        #region Events
        public event EventHandler HasChanged;
        #endregion

        #region Methodes
        private void NotifyChange()
        {
            HasChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
