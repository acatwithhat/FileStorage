using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;

namespace MvcApplication1.Models
{
    public class NHibertnateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var employeeConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Employee.hbm.xml");
            configuration.AddFile(employeeConfigurationFile);
            var documentConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Document.hbm.xml");
            configuration.AddFile(documentConfigurationFile);
            var authorConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Nhibernate\Author.hbm.xml");
            configuration.AddFile(authorConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}