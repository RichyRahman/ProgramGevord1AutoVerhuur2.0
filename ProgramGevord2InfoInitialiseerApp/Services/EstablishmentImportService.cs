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
    public class EstablishmentImportService
    {
        public (List<Establishment> validEstablishments, List<ImportError> errors) ImportEstablishments(string filePath)
        {
            var validEstablishments = new List<Establishment>();
            var errors = new List<ImportError>();
            var alphaOnlyRegex = new Regex(@"^[^\d]+$");
            var addressHasNumber = new Regex(@"\d");

            if (!File.Exists(filePath) || Path.GetExtension(filePath).ToLower() != ".csv")
            {
                throw new InvalidFileFormatException("Bestand niet gevonden of heeft geen .csv extensie.");
            }

            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2 || lines[0] != "Airport;Street;PostalCode;City;Country")
            {
                throw new InvalidFileFormatException("Ongeldige CSV-header voor vestigingen.");
            }

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var fields = line.Split(';');

                if (fields.Length != 5)
                {
                    errors.Add(new ImportError("Establishments.csv", i + 1, "Onjuist aantal kolommen."));
                    continue;
                }

                string airport = fields[0].Trim();
                string street = fields[1].Trim();
                string postalCode = fields[2].Trim();
                string city = fields[3].Trim();
                string country = fields[4].Trim();

                if (string.IsNullOrWhiteSpace(airport) || !alphaOnlyRegex.IsMatch(airport))
                {
                    errors.Add(new ImportError("Establishments.csv", i + 1, "Ongeldige of lege luchthavennaam."));
                    continue;
                }

                if (!addressHasNumber.IsMatch(street) || !addressHasNumber.IsMatch(postalCode))
                {
                    errors.Add(new ImportError("Establishments.csv", i + 1, "Straat of postcode bevat geen nummer."));
                    continue;
                }

                if (!alphaOnlyRegex.IsMatch(city) || !alphaOnlyRegex.IsMatch(country))
                {
                    errors.Add(new ImportError("Establishments.csv", i + 1, "Stad of land evat cijfers."));
                    continue;
                }

                validEstablishments.Add(new Establishment
                {
                    Airport = airport,
                    Street = street,
                    PostalCode = postalCode,
                    City = city,
                    Country = country
                });
            }

            return (validEstablishments, errors);
        }
    }
}
