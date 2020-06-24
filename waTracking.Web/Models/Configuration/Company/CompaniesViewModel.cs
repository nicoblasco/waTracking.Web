using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.Configuration.Company
{
    public class CompaniesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public bool Enabled { get; set; }
        public string InitialDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public string Address { get; set; }
        public string Postal { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Schedule { get; set; }
        public string Comment { get; set; }
        public string Logo { get; set; }
        public string LogoName { get; set; }
        public List<int> Sectors { get; set; }
    }
}
