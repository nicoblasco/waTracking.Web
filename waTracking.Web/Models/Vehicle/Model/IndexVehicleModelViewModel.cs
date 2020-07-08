using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.Vehicle.Model
{
    public class IndexVehicleModelViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public int VehicleBrandId { get; set; }

        public string Brand { get; set; }

        public string VehicleBrand { get; set; }
    }
}
