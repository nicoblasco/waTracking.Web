using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.Security
{
    public class SecurityRoleAction
    {
        public int Id { get; set; }
        public int SecurityRoleId { get; set; }
        public int SecurityActionId { get; set; }

        public virtual SecurityRole SecurityRole { get; set; }
        public virtual SecurityAction  SecurityAction { get; set; }
    }
}
