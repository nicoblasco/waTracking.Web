using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.Vehicle
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int VehicleBrandId { get; set; }
        public bool Enabled { get; set; }

        public VehicleBrand VehicleBrand { get; set; }
    }
}
