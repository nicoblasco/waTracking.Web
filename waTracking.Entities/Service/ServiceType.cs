using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.Service
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public bool Enabled { get; set; }
    }
}
