using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.System.RoleAction
{
    public class RoleActionViewModel
    {
        public int SystemRoleId { get; set; }
        public int SystemScreenId { get; set; }

        public int SystemActionId { get; set; }
        public string ActionCode { get; set; }
        public string ActionDescription { get; set; }
        public bool Enabled { get; set; }

    }
}
