using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.System.Action
{
    public class UpdateSystemActionViewModel
    {
        public int? Id { get; set; }
        public int SystemScreenId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public bool IsNew { get; set; }
        public bool IsRemoved { get; set; }
    }
}
