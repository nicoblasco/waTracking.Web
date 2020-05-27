using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.System.Field
{
    public class SystemScreenFieldViewModel
    {
        public int Id { get; set; }
        public int SystemScreenId { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public string FieldName { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public string DefaultValue { get; set; }

        public bool IsNew { get; set; }
        public bool IsRemoved { get; set; }
    }
}
