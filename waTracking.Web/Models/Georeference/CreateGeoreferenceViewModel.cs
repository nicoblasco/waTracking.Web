using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.Georeference
{
    public class CreateGeoreferenceViewModel
    {
        public string Identifier { get; set; }
        public Int64 TrackerLogId { get; set; }
        public int GeoTrackerId { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
