using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class TableRowQuantityComparer : IComparer<TableRow>
    {
        public int Compare(TableRow x, TableRow y)
        {
            if (x.TotalLineCount < y.TotalLineCount)
            {
                return  1;
            }
            if (x.TotalLineCount == y.TotalLineCount)
            {
                return  (new CaseInsensitiveComparer()).Compare(x.Character.Name, y.Character.Name);   
            }
            return -1;
        }
    }
}
