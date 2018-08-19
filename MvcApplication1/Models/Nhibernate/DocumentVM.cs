using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MvcApplication1.Models
{
    public class DocumentVM
    {
        public DocumentVM() { }
        public DocumentVM(Document doc)
        {
            Doc = doc;

        }
        
        [Required]
        public virtual Document Doc { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        public virtual HttpPostedFileBase File { get; set; }
    }
}