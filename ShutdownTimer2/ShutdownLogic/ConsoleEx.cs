using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownLogic
{
    class ConsoleEx
    {
        int line = 0;
        /// <summary>
        /// Debug mode only, print statements will not be executed in release mode.
        /// Provides the possibility for a print statement which prints the line number as prefix starting with 0.
        /// </summary>
        [Conditional("DEBUG")]
        public void Write(params string[] text)
        {
            Array.ForEach(text, t => Console.WriteLine($"{++line}:\t{t}"));
        }

        /// <summary>
        /// Debug mode only, print statements will not be executed in release mode.
        /// Provides the possibility for a print statement which prints the calling class as prefix.
        /// </summary>
        [Conditional("DEBUG")]
        public static void WriteCalling(Object calling, params string[] vars)
        {
            if (calling != null) {
                Console.Write($"DEBUG-log-by {calling.GetType().Name}\t: ");
            }
            else {
                Console.Write("$DEBUG-log-by unknown class\t: ");
            }
            Array.ForEach(vars, n => Console.Write($"{n} "));
            Console.WriteLine();
        }
    }
}
