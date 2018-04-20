using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    /// static only class for "printf debugging"
    class T
    {
        // print this only in Debug mode
        public static void TraceD(string err)
        {
#if DEBUG
            Trace(err);
#endif
        }

        // print this all the time
        public static void Trace(string err)
        {

            Console.WriteLine(err);

        }
    }
}
