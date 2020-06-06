using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using waTracking.Entities.System;

namespace waTracking.Entities.Configuration
{
    public class ConfigScreen
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public int Orden { get; set; }

        public string Icon { get; set; }

        public int CompanyId { get; set; }
        public int SystemScreenId { get; set; }
        public virtual Company Company { get; set; }
        public virtual SystemScreen SystemScreen { get; set; }



        public ICollection<ConfigScreenField> ConfigScreenFields { get; set; }
        public ICollection<waTracking.Entities.Security.SecurityAction> SecurityActions { get; set; }


    }
}
