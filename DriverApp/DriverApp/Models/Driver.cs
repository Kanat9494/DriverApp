using System;
using System.Collections.Generic;
using System.Text;

namespace DriverApp.Models
{
    internal class Driver
    {
        public int DriverId { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Itn { get; set; }
        public string Password { get; set; }
        public string LicencePlate { get; set; }
        public string BusNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }   
        public byte SignedId { get; set; }
    }
}
