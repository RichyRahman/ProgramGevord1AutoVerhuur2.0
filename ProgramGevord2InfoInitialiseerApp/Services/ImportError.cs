namespace ProgramGevord1InfoInitialiseerApp.Services
{
    public class ImportError
    {
        public string FileName { get; }
        public int LineNumber { get; }
        public string Message { get; }

        public ImportError(string fileName, int lineNumber, string message)
        {
            FileName = fileName;
            LineNumber = lineNumber;
            Message = message;
        }

        public override string ToString()
            => $"{FileName};{LineNumber};{Message}";
    }
}