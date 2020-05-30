using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.System.Role
{
    public class UpdateSystemRoleViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public bool Enabled { get; set; }
        public bool IsNew { get; set; }
        public bool IsRemoved { get; set; }
    }
}
