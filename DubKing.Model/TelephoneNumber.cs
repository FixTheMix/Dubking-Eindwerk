using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public enum PhonenumberType { Mobile, Home, Work}
    public class TelephoneNumber
    {
        #region Properties
        public PhonenumberType Type { get; set; }
        public int Number { get; set; }
        #endregion
        #region Constuctors
        /// <summary>
        /// Initializes a new instance of Telephonenumber
        /// </summary>
        public TelephoneNumber()
        {
            Type = PhonenumberType.Home;
            Number = 0;
        }
        /// <summary>
        /// Initializes a new instance of TelephoneNumber
        /// </summary>
        /// <param name="t">PhonenumberType</param>
        /// <param name="n">Phonenumber</param>
        public TelephoneNumber(PhonenumberType t, int n)
        {
            Type = t;
            Number = n;
        }
        #endregion
    }
}
