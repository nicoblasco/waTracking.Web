using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.Service
{
    public class Service
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ServiceTypeId { get; set; }
        public bool Enabled { get; set; }

        public ServiceType ServiceType { get; set; }

    }
}
