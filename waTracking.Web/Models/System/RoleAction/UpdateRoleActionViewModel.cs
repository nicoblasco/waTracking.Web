using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.System.RoleAction
{
    public class UpdateRoleActionViewModel
    {
        public int SystemRoleId { get; set; }

        public int SystemActionId { get; set; }
        public bool Enabled { get; set; }
    }
}
