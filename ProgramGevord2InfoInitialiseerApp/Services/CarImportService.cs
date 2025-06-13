using Autoverhuur.Domain.Exceptions;
using Autoverhuur.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ProgramGevord1InfoInitialiseerApp.Services
{
    public class CarImportService
    {
        private static readonly string[] AllowedMotorTypes = { "Hybrid", "Gasoline", "Electric", "Diesel" };

        public (List<Car> validCars, List<ImportError> errors) ImportCars(string filePath)
        {
            var validCars = new List<Car>();
            var errors = new List<ImportError>();
            var seenPlates = new HashSet<string>();

            if (!File.Exists(filePath) || Path.GetExtension(filePath).ToLower() != ".csv")
            {
                throw new InvalidFileFormatException("Bestand niet gevonden of heeft geen .csv extensie.");
            }

            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2 || lines[0] != "LicensePlate;Model;Seats;Motortype")
            {
                throw new InvalidFileFormatException("Ongeldige CSV-header voor auto's.");
            }

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var fields = line.Split(';');

                if (fields.Length != 4)
                {
                    errors.Add(new ImportError("Cars.csv", i + 1, "Onjuist aantal kolommen."));
                    continue;
                }

                string plate = fields[0].Trim();
                string model = fields[1].Trim();
                bool seatsParsed = int.TryParse(fields[2], out int seats);
                string motorType = fields[3].Trim();

                if (string.IsNullOrWhiteSpace(plate) || seenPlates.Contains(plate))
                {
                    errors.Add(new ImportError("Cars.csv", i + 1, "Nummerplaat ontbreekt of is niet uniek."));
                    continue;
                }

                if (!seatsParsed || seats < 2 || seats > 10)
                {
                    errors.Add(new ImportError("Cars.csv", i + 1, "Ongeldig aantal zitplaatsen."));
                    continue;
                }

                if (!AllowedMotorTypes.Contains(motorType))
                {
                    errors.Add(new ImportError("Cars.csv", i + 1, "Ongeldig motortype."));
                    continue;
                }

                seenPlates.Add(plate);
                validCars.Add(new Car
                {
                    LicensePlate = plate,
                    Model = model,
                    Seats = seats,
                    MotorType = motorType
                });
            }

            return (validCars, errors);
        }
    }
}
