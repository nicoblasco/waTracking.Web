﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.Vehicle.Model
{
    public class CreateVehicleModelViewModel
    {
        public string Description { get; set; }
        public int VehicleBrandId { get; set; }
        public int CompanyId { get; set; }
    }
}
