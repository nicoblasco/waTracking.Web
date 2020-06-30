using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.System.Logs
{
    public class CreationLogErrorViewModel
    {
        public int CompanyId { get; set; }
        public int SecurityUserId { get; set; }
        public string Path { get; set; }
        public string Error { get; set; }
    }
}
