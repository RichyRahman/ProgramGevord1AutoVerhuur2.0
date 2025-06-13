namespace ProgramGevord1InfoInitialiseerApp.Controller
{
    public class ImportSummary
    {
        public int EstablishmentsImported { get; }
        public int CustomersImported { get; }
        public int CarsImported { get; }
        public int ErrorCount { get; }

        public ImportSummary(int est, int cust, int cars, int errors)
        {
            EstablishmentsImported = est;
            CustomersImported = cust;
            CarsImported = cars;
            ErrorCount = errors;
        }
    }
}