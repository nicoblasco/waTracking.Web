using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Entities.Department
{
    public class DepartmentChild
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public bool Enabled { get; set; }

        public Department Deparment { get; set; }
    }
}
