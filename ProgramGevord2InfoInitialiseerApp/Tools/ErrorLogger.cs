using ProgramGevord1InfoInitialiseerApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGevord1InfoInitialiseerApp.Tools
{
    public class ErrorLogger
    {
        public static void WriteErrorsToCsv(List<ImportError> errors, string path)
        {
            using var writer = new StreamWriter(path);
            writer.WriteLine("FileName;LineNumber;Message");
            foreach (var error in errors)
            {
                writer.WriteLine(error.ToString());
            }
        }
    }
}
