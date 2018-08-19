using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication1.Models;

namespace MvcApplication1.Models
{
    public class EmployeeVM
    {
        public Employee Emp { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}