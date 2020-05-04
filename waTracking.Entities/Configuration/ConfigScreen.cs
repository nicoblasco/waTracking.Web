using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.Configuration
{
    public class ConfigScreen
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Entity { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }



        public ICollection<ConfigScreenField> ConfigScreenFields { get; set; }


    }
}
