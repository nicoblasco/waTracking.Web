using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.Security.Action
{
    public class SecurityActionViewModel
    {
        public int Id { get; set; }
        public int SystemScreenId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public bool IsNew { get; set; }
        public bool IsRemoved { get; set; }
    }
}
