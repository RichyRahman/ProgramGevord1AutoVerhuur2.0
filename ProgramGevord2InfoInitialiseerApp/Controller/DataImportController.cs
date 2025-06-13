using ProgramGevord1InfoInitialiseerApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGevord1InfoInitialiseerApp.Controller
{
    public class DataImportController
    {
        private readonly EstablishmentImportService _establishmentService;
        private readonly CustomerImportService _customerService;
        private readonly CarImportService _carService;
        private readonly EstablishmentRepository _establishmentRepo;
        private readonly CustomerRepository _customerRepo;
        private readonly CarRepository _carRepo;

        public DataImportController(
            EstablishmentImportService establishmentService,
            CustomerImportService customerService,
            CarImportService carService,
            EstablishmentRepository establishmentRepo,
            CustomerRepository customerRepo,
            CarRepository carRepo)
        {
            _establishmentService = establishmentService;
            _customerService = customerService;
            _carService = carService;
            _establishmentRepo = establishmentRepo;
            _customerRepo = customerRepo;
            _carRepo = carRepo;
        }

        public ImportSummary ImportAll(string estPath, string custPath, string carPath, string errorOutputPath)
        {
            var (establishments, estErrors) = _establishmentService.ImportEstablishments(estPath);
            _establishmentRepo.SaveEstablishments(establishments);

            var (customers, custErrors) = _customerService.ImportCustomers(custPath);
            _customerRepo.SaveCustomers(customers);

            var (cars, carErrors) = _carService.ImportCars(carPath);
            _carRepo.SaveCars(cars, establishments);

            var allErrors = new List<ImportError>();
            allErrors.AddRange(estErrors);
            allErrors.AddRange(custErrors);
            allErrors.AddRange(carErrors);

            if (allErrors.Any())
            {
                ErrorLogger.WriteErrorsToCsv(allErrors, errorOutputPath);
            }

            return new ImportSummary(
                establishments.Count,
                customers.Count,
                cars.Count,
                allErrors.Count
                );
        }
    }
}
