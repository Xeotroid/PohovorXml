using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd {
    static class StrExtensions {
        public static string FirstUpper(this string str) {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
    }
}
