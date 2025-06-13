using Autoverhuur.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoverhuur.Persistentie
{
    public class CarRepository
    {
        private readonly SqlConnection _connection;

        public CarRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void SaveCars(List<Car> cars, List<Establishment> establishments)
        {
            int vestigingIndex = 0;

            for (int i = 0; i < cars.Count; i++)
            {
                var car = cars[i];
                var establishmentId = vestigingIndex + 1;
                vestigingIndex = (vestigingIndex + 1) % establishments.Count;

                var cmd = new SqlCommand("INSERT INTO Cars (LicensePlate, Model Seats, MotorType, EstablishmentId) VALUES (@plate, @model, @seats, @motor, @eid)", _connection);
                cmd.Parameters.AddWithValue("@plate", car.LicensePlate);
                cmd.Parameters.AddWithValue("@model", car.Model);
                cmd.Parameters.AddWithValue("@seats", car.Seats);
                cmd.Parameters.AddWithValue("@motor", car.MotorType);
                cmd.Parameters.AddWithValue("@eid", establishmentId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
