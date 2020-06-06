using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.System;

namespace waTracking.Entities.Configuration
{
    public class ConfigScreenField
    {
        public int Id { get; set; }
        public int ConfigScreenId { get; set; }
        public int SystemScreenFieldId { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public string FieldName { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public string DefaultValue { get; set; }

        
        public virtual ConfigScreen ConfigScreen { get; set; }
        public virtual SystemScreenField SystemScreenField { get; set; }
    }
}
