using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Autoverhuur.Tests
{
    public  class DomainTests
    {
        [Fact]
        public void CarImport_RejectsInvalidSeats()
        {
            var service = new CarImportService();
            var path = "invalid_car.csv";
            File.WriteAllText(path, "LicensePlate;Model;Seats;Motortype\n1-AAA-111;ModelX;15;Gasoline");

            var (_, errors) = service.ImportCars(path);

            Assert.Single(errors);
            Assert.Contains("zitplaatsen", errors[0].Message);
        }

        [Fact]
        public void CustomerImport_RejectsInvalidEmail()
        {
            var service = new CustomerImportService();
            var path = "invalid_customer.csv";
            File.WriteAllText(path, "FirstName;LastName;Email;Street;PostalCode;City;Country\nLies;Janssen;invalidemail;Teststraat 12;3000;Leuven;België");

            var (_, errors) = service.ImportCustomers(path);

            Assert.Single(errors);
            Assert.Contains("e-mailadres", errors[0].Message);
        }

        [Fact]
        public void EstablishmentImport_RejectsEmptyAirport()
        {
            var service = new EstablishmentImportService();
            var path = "invalid_est.csv";
            File.WriteAllText(path, "Airport;Street;PostalCode;City;Country\n;Hoofdstraat 1;1234;Gent;België");

            var (_, errors) = service.ImportEstablishments(path);

            Assert.Single(errors);
            Assert.Contains("luchthavennaam", errors[0].Message);
        }

        [Fact]
        public void CarImport_AcceptsValidCar()
        {
            var service = new CarImportService();
            var path = "valid_car.csv";
            File.WriteAllText(path, "LicensePlate;Model;Seats;Motortype\n1-XYZ-123;Golf;5;Diesel");

            var (cars, errors) = service.ImportCars(path);

            Assert.Single(cars);
            Assert.Empty(errors);
            Assert.Equal("Golf", cars[0].Model);
        }

        [Fact]
        public void CustomerImport_RejectsPostalCodeWithoutNumber()
        {
            var service = new CustomerImportService();
            var path = "invalid_postal.csv";
            File.WriteAllText(path, "FirstName;LastName;Email;Street;PostalCode;City;Country\nJan;Peeters;jan@peeters.be;Hoofdstraat;ABCD;Brussel;België");

            var (_, errors) = service.ImportCustomers(path);

            Assert.Single(errors);
            Assert.Contains("postcode bevat geen nummer", errors[0].Message);
        }
    }
}
