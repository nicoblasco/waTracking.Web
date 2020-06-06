using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.Configuration.Company
{
    public class RoleCompanyViewModel
    {

        public string Name { get; set; }

        public string Description { get; set; }
        public bool Enabled { get; set; }

        public int CompanyId { get; set; }
        public int SystemRoleId { get; set; }

        public List<RoleActionCompanyViewModel> SecurityRoleActions { get; set; }
        public List<RoleScreenCompanyViewModel> SecurityRoleScreens { get; set; }
    }
}
