using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Configuration;

namespace waTracking.Entities.Service
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public bool Enabled { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
