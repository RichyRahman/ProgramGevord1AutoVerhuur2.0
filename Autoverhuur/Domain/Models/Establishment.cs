using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoverhuur.Domain.Models
{
    public partial class Establishment
    {
        public required string Airport {  get; set; }
        public required string Street { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public int Id { get; set; }
    }
}
