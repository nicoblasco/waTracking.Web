using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.System
{
    public class SystemRoleScreen
    {
        public int Id { get; set; }
        public int SystemRoleId { get; set; }
        public int SystemScreenId { get; set; }

        public virtual SystemRole SystemRole { get; set; }
        public virtual SystemScreen SystemScreen { get; set; }
    }
}
