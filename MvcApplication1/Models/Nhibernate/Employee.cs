using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Employee
    {
        public Employee() { }
        public Employee(EmployeeVM nemploye)
        {
            this.FirstName = nemploye.Emp.FirstName;
            this.LastName = nemploye.Emp.LastName;
            this.Designation = nemploye.Emp.Designation;
       
            byte[] bfile = new byte[nemploye.File.ContentLength];
            nemploye.File.InputStream.Read(bfile, 0, nemploye.File.ContentLength);
            this.BFile = bfile;
        }
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Designation { get; set; }
        public virtual byte[] BFile { get; set; }
        
    }
}