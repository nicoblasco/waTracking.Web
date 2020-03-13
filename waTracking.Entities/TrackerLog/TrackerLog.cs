using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.TrackerLog
{
    public class TrackerLog
    {
        public Int64 Id { get; set; }
        public DateTime CreationDate { get; set; }

        public string Message { get; set; }
    }
}
