using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using waTracking.Web.Models.Security.RoleAction;
using waTracking.Web.Models.Security.RoleScreen;

namespace waTracking.Web.Models.Security.Role
{
    public class SecurityRolViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public bool Enabled { get; set; }
        public bool IsNew { get; set; }
        public bool IsRemoved { get; set; }

        public List<SecurityRoleActionViewModel> SecurityRoleActions { get; set; }
        public List<SecurityRoleScreenViewModel> SecurityRoleScreens { get; set; }
    }
}
