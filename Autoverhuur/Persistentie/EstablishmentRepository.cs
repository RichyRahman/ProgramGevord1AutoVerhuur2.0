using Autoverhuur.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoverhuur.Persistentie
{
    public class EstablishmentRepository
    {
        private readonly SqlConnection _connection;

        public EstablishmentRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void SaveEstablishments(List<Establishment> establishments)
        {
            foreach (var est in establishments)
            {
                var cmd = new SqlCommand("INSERT INTO Establishments (Airport, Street, PostalCode, City, Country) VALUES (@a, @s, @p, @c, @co)", _connection);
                cmd.Parameters.AddWithValue("@a", est.Airport);
                cmd.Parameters.AddWithValue("@s", est.Street);
                cmd.Parameters.AddWithValue("@p", est.PostalCode);
                cmd.Parameters.AddWithValue("@c", est.City);
                cmd.Parameters.AddWithValue("@co", est.Country);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
