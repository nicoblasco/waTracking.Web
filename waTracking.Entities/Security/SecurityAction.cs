using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Configuration;

namespace waTracking.Entities.Security
{
    public class SecurityAction
    {
        public int Id { get; set; }
        public int ConfigScreenId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public virtual ConfigScreen ConfigScreen { get; set; }
    }
}
