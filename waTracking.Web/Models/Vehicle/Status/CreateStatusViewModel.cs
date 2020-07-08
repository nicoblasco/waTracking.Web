using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.Vehicle.Status
{
    public class CreateStatusViewModel
    {
        public string Description { get; set; }
        public int CompanyId { get; set; }
    }
}
