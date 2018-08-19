using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Author
    {
        virtual public int Id { get; set; }
        virtual public string Login { get; set; }
        virtual public string Password { get; set; }
        virtual public bool Remember { get; set; }
    }
}