using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.System
{
   public class SystemScreen
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

        public ICollection<SystemScreenField> SystemScreenFields { get; set; }
        public ICollection<SystemAction> SystemActions { get; set; }
    }
}
