using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.Georeference
{
    public class Georeference
    {
        public Int64 Id { get; set; }
        public string Identifier { get; set; }
        public Int64 TrackerLogId { get; set; }
        public int GeoTrackerId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Enabled { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public virtual TrackerLog.TrackerLog TrackerLog { get; set; }
        public virtual GeoTracker.GeoTracker GeoTracker { get; set; }
    }
}
