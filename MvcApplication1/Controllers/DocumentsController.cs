﻿using System;
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
                    
                    List<Document> documents_search = new List<Document>();

                    IList<Document> documents = session.Query<Document>().Fetch(c => c.Author).ToList();

                    if (name != null & name != "")
                    {
                        documents = session.QueryOver<Document>().Where(c => c.Name == name).Fetch(c => c.Author).Eager.List();
                    }


                    foreach (var item in documents)
                    {
                        if (item.Name.Length > 30) item.Name = item.Name.Substring(0, 30)+"...";
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
        [Authorize]
        public ActionResult Create(DocumentVM docVM)
        {
            try
            {
                using (ISession session = NHibertnateSession.OpenSession())
                {
                    ADocumentCreator DocCreator = new DocumentCreator();
                    ADocument doc = DocCreator.FM_Create_Doc(docVM);

                    var add_doc = session.GetNamedQuery("AddDoc");
                    string cur_name = System.Web.HttpContext.Current.User.Identity.Name;
                    var current_author = session.QueryOver<Author>().Where(c => c.Login == cur_name).List();
                    
                    add_doc.SetParameter("name_new", doc.Name);
                    add_doc.SetParameter("date_new", doc.Date);
                    add_doc.SetParameter("filename_new", doc.FileName);
                    add_doc.SetParameter("author_id", current_author[0]);
                    add_doc.ExecuteUpdate();

                    var docs = session.Query<Document>().ToList();

                    
                    if (docVM.File != null)
                    {
                        string path = "~/App_Data/Files/";
                        string fileName =docs[docs.Count-1].Id +"_"
                            +System.IO.Path.GetFileName(docVM.File.FileName);
                        docVM.File.SaveAs(Server.MapPath(path+fileName));
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
                    string file = @"~/App_Data/Files/"+doc.Id + "_"+doc.FileName;
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
