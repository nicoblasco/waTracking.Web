using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.System
{
    public class SystemRoleAction
    {
        public int Id { get; set; }
        public int SystemRoleId { get; set; }
        public int SystemActionId { get; set; }

        public virtual SystemRole  SystemRole { get; set; }
        public virtual SystemAction SystemAction { get; set; }
    }
}
