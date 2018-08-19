using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication1.Models
{
    public class Document
    {
        public Document() { }
        public Document(DocumentVM docVM)
        {
            this.Name = docVM.Doc.Name;
            this.Date = System.DateTime.Now;
            this.FileName = docVM.File.FileName;
        }
        public virtual int Id { get; set; }
        public virtual Author Author { get; set; }
        [Required]
        public virtual string Name { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string FileName { get; set; }
        
    }

    
    
}