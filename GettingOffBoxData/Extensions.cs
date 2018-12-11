using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingOffBoxData
{
    public static class Extensions
    {
        public static bool Validate(this string str)
        {
            return ((str != null) && (str.TrimStart(new char[0]).Length > 0));
        }
    }
}
