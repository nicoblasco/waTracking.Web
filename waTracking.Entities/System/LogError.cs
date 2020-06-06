using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.System
{
    public class LogError
    {
        public Int64 Id { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Error { get; set; }
        public string Comment { get; set; }
    }
}
