using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.Configuration
{
    public class CompanySector
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int SectorId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Sector Sector { get; set; }
    }
}
