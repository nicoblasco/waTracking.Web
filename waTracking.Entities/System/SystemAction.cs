using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.System
{
    public class SystemAction
    {
        public int Id { get; set; }
        public int SystemScreenId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public virtual SystemScreen SystemScreen { get; set; }
    }
}
