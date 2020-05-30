using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.System.RoleScreen
{
    public class SystemRoleScreenViewModel
    {
        public int Id { get; set; }
        public int SystemRoleId { get; set; }
        public int SystemScreenId { get; set; }
        public int? ParentId { get; set; }
        public string ScreenName { get; set; }
        public bool Enabled { get; set; }
    }
}
