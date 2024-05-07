using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faculty_Helper
{
    public static class Globle
    {
        static String txt1;
        public static void setText(String str)
        {
            txt1 = str;
        }
        public static String getText()
        {
            return txt1;
        }
    }
   

}
