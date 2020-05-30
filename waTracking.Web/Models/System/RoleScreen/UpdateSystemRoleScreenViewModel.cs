using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.System.RoleScreen
{
    public class UpdateSystemRoleScreenViewModel
    {
        public int SystemRoleId { get; set; }
        public int SystemScreenId { get; set; }
        public bool Enabled { get; set; }
    }
}
