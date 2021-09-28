using System;
using System.Collections.Generic;
using System.Text;

namespace DespPlus.Utils
{
    public static class StringExtention
    {
        public static bool LimpoNuloBranco(this String s)
        {
            return String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s);
        }
    }
}
