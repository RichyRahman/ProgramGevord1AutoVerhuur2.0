using Autoverhuur.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoverhuur.Persistentie
{
    public class CustomerRepository
    {
        private readonly SqlConnection _connection;

        public CustomerRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void SaveCustomers(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                var cmd = new SqlCommand("INSERT INTO Customers (FirstName, LastName, Email, Street, PostalCode, City, Country) VALUES (@fn, @ln, @em, @st, @pc, @ct, @co)", _connection);
                cmd.Parameters.AddWithValue("@fn", customer.FirstName);
                cmd.Parameters.AddWithValue("@ln", customer.LastName);
                cmd.Parameters.AddWithValue("@em", customer.Email);
                cmd.Parameters.AddWithValue("@st", customer.Street);
                cmd.Parameters.AddWithValue("@pc", customer.PostalCode);
                cmd.Parameters.AddWithValue("@ct", customer.City);
                cmd.Parameters.AddWithValue("@co", customer.Country);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
