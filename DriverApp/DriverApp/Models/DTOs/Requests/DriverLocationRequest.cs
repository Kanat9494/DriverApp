using System;
using System.Collections.Generic;
using System.Text;

namespace DriverApp.Models.DTOs.Requests
{
    internal class DriverLocationRequest
    {
        public int DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
