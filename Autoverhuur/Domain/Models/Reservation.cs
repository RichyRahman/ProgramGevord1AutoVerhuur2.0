using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoverhuur.Domain.Models
{
    public class Reservation
    {
        public required Customer Customer { get; set; }
        public required Car Car { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Id { get; set; }

        public required Establishment Establishment { get; set; }

        public string Period => $"{StartDate:dd/MM/yyyy} - {EndDate:dd/MM/yyyy}";
    }
}
