using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.GeoTracker
{
    public class GeoTracker
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string GpsModel { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
