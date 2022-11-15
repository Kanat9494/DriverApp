using System;
using System.Collections.Generic;
using System.Text;

namespace DriverApp.Models.DTOs.Responses
{
    public class DriverResponse
    {
        public int DriverId { get; set; }
        public string UserName { get; set; }
        public string BusNumber { get; set; }
    }
}
