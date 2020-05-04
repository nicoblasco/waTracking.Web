using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.Security.RoleAction
{
    public class RoleActionViewModel
    {
        public int Id { get; set; }
        public int SecurityRoleId { get; set; }
        public int ActionId { get; set; }
        public string ActionCode { get; set; }
    }
}
