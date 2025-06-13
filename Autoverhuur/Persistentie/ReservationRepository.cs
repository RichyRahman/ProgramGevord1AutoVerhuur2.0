using Autoverhuur.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoverhuur.Persistentie
{
    public class ReservationRepository
    {
        private readonly SqlConnection _connection;
        public ReservationRepository(SqlConnection connection) => _connection = connection;

        public void SaveReservation(Reservation res)
        {
            var cmd = new SqlCommand("INSERT INTO Reservations (CustomerId, CarId, StartDate, EndDate) VALUES (@c, @a, @s, @e)", _connection);
            cmd.Parameters.AddWithValue("@c", res.Customer.Id);
            cmd.Parameters.AddWithValue("@a", res.Car.Id);
            cmd.Parameters.AddWithValue("@s", res.StartDate);
            cmd.Parameters.AddWithValue("@e", res.EndDate);
            cmd.ExecuteNonQuery();
        }
    }
}
