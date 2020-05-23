using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.System.Screen
{
    public class SystemScreenViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public bool IsDefault { get; set; }


        public int? ParentId { get; set; }
        public int Orden { get; set; }
        public string Entity { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }

        public bool IsNew { get; set; }
        public bool IsRemoved { get; set; }
    }
}
