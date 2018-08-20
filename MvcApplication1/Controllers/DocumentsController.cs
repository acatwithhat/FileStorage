using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using NHibernate;
using MvcApplication1.Models;
using Microsoft.AspNet.Identity;
using WebMatrix.WebData;



namespace MvcApplication1.Controllers
{
    public class DocumentsController : Controller
    {
        //
        // GET: /Documents/

        [Authorize]
        public ActionResult Index(string name)
        {
            try
            {
                using (ISession session = NHibertnateSession.OpenSession())
                {
                    
                    var documents = session.Query<Document>().Fetch(c=>c.Author).ToList();
                    List<Document> documents_search = new List<Document>();
                    foreach (var item in documents)
                    {
                        if (item.Name.Length > 30) item.Name = item.Name.Substring(0, 30)+"...";
                    }
                    if (name != null & name!="")
                    {
                        foreach (var item in documents)
                        {
                            if (item.Name == name) documents_search.Add(item);
                        }
                        return View(documents_search);
                    }
                    return View(documents);
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DocumentVM docVM)
        {
            try
            {
                using (ISession session = NHibertnateSession.OpenSession())
                {
                    Document doc = new Document(docVM);
                    var add_doc = session.GetNamedQuery("AddDoc");
                    string cur_name = System.Web.HttpContext.Current.User.Identity.Name;
                    var current_author = session.QueryOver<Author>().Where(c => c.Login == cur_name).List();
                    //add_doc.SetParameter("id_new", doc.Id);
                    add_doc.SetParameter("name_new", doc.Name);
                    add_doc.SetParameter("date_new", doc.Date);
                    add_doc.SetParameter("filename_new", doc.FileName);
                    add_doc.SetParameter("author_id", current_author[0]);
                    add_doc.ExecuteUpdate();

                    if (docVM.File != null)
                    {
                        string fileName = docVM.Doc.Name+"_"+System.IO.Path.GetFileName(docVM.File.FileName);
                        docVM.File.SaveAs(Server.MapPath("~/App_Data/Files/" + fileName));
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            
        }

        [Authorize]
        public ActionResult OpenFile(Document document)
        {
            try
            {
                using (ISession session = NHibertnateSession.OpenSession())
                {
                    var doc = session.Get<Document>(document.Id);
                    string file = @"~/App_Data/Files/"+doc.Name + "_"+doc.FileName;
                    return File(file,System.Net.Mime.MediaTypeNames.Application.Octet,doc.FileName);
                }

            }
            catch (Exception exc)
            {
                return View();
            }

        }
       

        

    }
}
