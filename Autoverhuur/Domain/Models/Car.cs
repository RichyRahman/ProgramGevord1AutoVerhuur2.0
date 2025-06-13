using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoverhuur.Domain.Models
{
    public partial class Car
    {
        public required string LicensePlate { get; set; }
        public required string Model { get; set; }
        public required int Seats { get; set; }
        public required string MotorType { get; set; }
        public int Id { get; set; }
    }
}
