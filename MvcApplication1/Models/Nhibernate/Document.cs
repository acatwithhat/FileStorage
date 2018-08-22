using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication1.Models
{
    public abstract class ADocument
    {
        public abstract int Id { get; set; }
        public abstract Author Author { get; set; }
        public abstract string Name { get; set; }
        public abstract DateTime? Date { get; set; }
        public abstract string FileName { get; set; }
    }

    public abstract class ADocumentCreator
    {
        public abstract ADocument FM_Create_Doc(DocumentVM doc);
    }

    public class DocumentCreator : ADocumentCreator
    {
        public override ADocument FM_Create_Doc(DocumentVM doc)
        {
            return new Document(doc);
        }
    }


    public class Document:ADocument
    {
        public Document() { }
        public Document(DocumentVM docVM)
        {
            this.Name = docVM.Doc.Name;
            this.Date = System.DateTime.Now;
            this.FileName = docVM.File.FileName;
        }
        public override int Id { get; set; }
        public override Author Author { get; set; }
        [Required]
        public override string Name { get; set; }
        public override DateTime? Date { get; set; }
        public override string FileName { get; set; }
        
    }

}