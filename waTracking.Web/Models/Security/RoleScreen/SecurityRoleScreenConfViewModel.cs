using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using waTracking.Web.Models.Security.RoleAction;
using waTracking.Web.Models.Security.ScreenField;

namespace waTracking.Web.Models.Security.RoleScreen
{
    public class SecurityRoleScreenConfViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public int Orden { get; set; }

        public string Icon { get; set; }


        public string Path { get; set; }
        public int? ParentId { get; set; }
        public int SystemScreenId { get; set; }

        public List<ScreenFieldConfViewModel> Fields { get; set; }
        public List<SecurityRoleActionConfViewModel> Actions { get; set; }
    }
}
