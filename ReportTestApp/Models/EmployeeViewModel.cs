using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportTestApp.Models
{
    public class EmployeeViewModel
    {
        public Int32 id { get; set; }
        public String name { get; set; }
        public String gender { get; set; }
        public String designation { get; set; }
        public String department { get; set; }
        //public DateTime dob { get; set; }
    }
}