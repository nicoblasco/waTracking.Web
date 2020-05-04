using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Security;

namespace waTracking.Entities.Configuration
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Enabled { get; set; }

        public ICollection<SecurityUser> Usuarios { get; set; }
        public ICollection<ConfigScreen> ConfigScreen { get; set; }
    }
}
