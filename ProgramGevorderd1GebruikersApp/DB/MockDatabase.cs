using Autoverhuur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGevorderd1GebruikersApp.DB
{
    public static class MockDatabase
    {
        private static List<Customer> _customers = new();
        private static List<Establishment> _establishments = new();
        private static List<Car> _cars = new();
        private static List<Reservation> _reservations = new();

        static MockDatabase()
        {
            SeedData();
        }

        public static List<Customer> GetCustomers() => _customers;
        public static List<Establishment> GetEstablishments() => _establishments;

        public static void SaveReservation(Reservation res)
        {
            _reservations.Add(res);
        }

        public static void DeleteReservation(Reservation res)
        {
            _reservations.Remove(res);
        }

        public static List<Reservation> SearchReservations(string name, DateTime? date, Establishment location)
        {
            return _reservations.Where(r =>
                (string.IsNullOrEmpty(name) ||
                 r.Customer.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                 r.Customer.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (!date.HasValue || (r.StartDate <= date && r.EndDate >= date)) &&
                (location == null || r.Car.EstablishmentId == location.Id)
            ).ToList();
        }

        public static List<Car> SearchAvailableCars(Establishment location, DateTime start, DateTime end, int seats = 0)
        {
            return _cars.Where(c =>
                c.EstablishmentId == location.Id &&
                (seats == 0 || c.Seats >= seats) &&
                !_reservations.Any(r => r.Car.LicensePlate == c.LicensePlate && r.StartDate < end && r.EndDate > start)
            ).ToList();
        }

        public static List<Car> GetAvailableCarsWithContext(Establishment est, DateTime date)
        {
            return _cars.Where(c => c.EstablishmentId == est.Id)
                        .Select(c =>
                        {
                            var rPrev = _reservations
                                .Where(r => r.Car.LicensePlate == c.LicensePlate && r.EndDate <= date)
                                .OrderByDescending(r => r.EndDate).FirstOrDefault();

                            var rNext = _reservations
                                .Where(r => r.Car.LicensePlate == c.LicensePlate && r.StartDate >= date)
                                .OrderBy(r => r.StartDate).FirstOrDefault();

                            c.PreviousReservation = rPrev;
                            c.NextReservation = rNext;
                            return c;
                        }).ToList();
        }

        private static void SeedData()
        {
            _establishments = new List<Establishment>
            {
                new Establishment { Id = 1, Airport = "Zaventem", Street = "Leopoldlaan", PostalCode = "1930", City = "Brussel", Country = "België" },
                new Establishment { Id = 2, Airport = "Schiphol", Street = "Evert van de Beekstraat", PostalCode = "1118 CP", City = "Amsterdam", Country = "Nederland" }
            };

            _customers = new List<Customer>
            {
                new Customer { FirstName = "Jan", LastName = "Peeters", Email = "jan@peeters.be", Street = "Kerkstraat 12", PostalCode = "2000", City = "Antwerpen", Country = "België" },
                new Customer { FirstName = "Lotte", LastName = "Jansen", Email = "lotte.jansen@gmail.com", Street = "Boomstraat 5", PostalCode = "1000", City = "Brussel", Country = "België" }
            };

            _cars = new List<Car>
            {
                new Car { LicensePlate = "1-ABC-123", Model = "Renault Clio", Seats = 5, MotorType = "Gasoline", EstablishmentId = 1 },
                new Car { LicensePlate = "2-XYZ-789", Model = "Tesla Model 3", Seats = 5, MotorType = "Electric", EstablishmentId = 2 },
                new Car { LicensePlate = "3-DEF-456", Model = "Peugeot 208", Seats = 4, MotorType = "Diesel", EstablishmentId = 1 }
            };

            _reservations = new List<Reservation>();
        }
    }

    public class CarExtensions
    {
        public required Reservation PreviousReservation { get; set; }
        public required Reservation NextReservation { get; set; }
    }
}
}
