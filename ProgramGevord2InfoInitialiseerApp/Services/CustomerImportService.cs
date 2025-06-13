using Autoverhuur.Domain.Exceptions;
using Autoverhuur.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgramGevord1InfoInitialiseerApp.Services
{
    public class CustomerImportService
    {
        public (List<Customer> validCustomers, List<ImportError> errors) ImportCustomers(string filePath)
        {
            var validCustomers = new List<Customer>();
            var errors = new List<ImportError>();
            var seenEmails = new HashSet<string>();
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            var alphaOnlyRegex = new Regex(@"^[^\d]+$");
            var addressHasNumber = new Regex(@"\d");

            if (!File.Exists(filePath) || Path.GetExtension(filePath).ToLower() != ".csv")
            {
                throw new InvalidFileFormatException("Bestand niet gevonden of heeft geen .csv extensie.");
            }

            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2 || lines[0] != "FirstName;LastName;Email;Street;PostalCode;City;Country")
            {
                throw new InvalidFileFormatException("Ongeldige CSV-header voor klanten.");
            }

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var fields = line.Split(';');

                if (fields.Length != 7)
                {
                    errors.Add(new ImportError("Customers.csv", i + 1, "Onjuist aantal kolommen."));
                    continue;
                }

                string firstName = fields[0].Trim();
                string lastName = fields[1].Trim();
                string email = fields[2].Trim();
                string street = fields[3].Trim();
                string postalCode = fields[4].Trim();
                string city = fields[5].Trim();
                string country = fields[6].Trim();

                if (!string.IsNullOrWhiteSpace(email) || !emailRegex.IsMatch(email) || seenEmails.Contains(email))
                {
                    errors.Add(new ImportError("Customer.csv", i + 1, "Ongeldig of niet-uniek e-mailadres."));
                    continue;
                }

                if (!addressHasNumber.IsMatch(street) || !addressHasNumber.IsMatch(postalCode))
                {
                    errors.Add(new ImportError("Customers.csv", i + 1, "Straat of postcode bevat geen nummer."));
                    continue;
                }

                if (!alphaOnlyRegex.IsMatch(firstName) || !alphaOnlyRegex.IsMatch(lastName) ||
                    !alphaOnlyRegex.IsMatch(city) || !alphaOnlyRegex.IsMatch(country))
                {
                    errors.Add(new ImportError("Customers.csv", i + 1, "Naam, stad of land bevat cijfers."));
                    continue;
                }

                seenEmails.Add(email);
                validCustomers.Add(new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Street = street,
                    PostalCode = postalCode,
                    City = city,
                    Country = country
                });
            }

            return (validCustomers, errors);
        }
    }
}
