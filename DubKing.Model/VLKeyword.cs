using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class VLKeyword : IComparable<VLKeyword>
    {

        #region Properties

        public int KeywordId { get; set; }
        public string KeyWord { get; set; }

        #endregion

        public VLKeyword()
        {

        }
        public VLKeyword(string keyword)
        {
            KeyWord = keyword;
        }

        public int CompareTo(VLKeyword other)
        {
            if (other == null) return 1;
            return KeywordId.CompareTo(other.KeywordId);
        }

        public static bool operator== (VLKeyword left, VLKeyword right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }
            if (ReferenceEquals(right,null))
            {
                return false;
            }
                return left.KeywordId.Equals(right.KeywordId);
            
        }
        public static bool operator!= (VLKeyword left, VLKeyword right)
        {
            if (ReferenceEquals(left, null))
            {
                return !ReferenceEquals(right, null);
            }
            if (ReferenceEquals(right, null))
            {
                return true;
            }
            return !left.KeywordId.Equals(right.KeywordId);
        }
    }
}
