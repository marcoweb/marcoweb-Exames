using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MarcoCarvalho.Exames.Data.Common;
using System.Web.Configuration;

namespace MarcoCarvalho.Exames.WebApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            string host = WebConfigurationManager.AppSettings["dbHost"];
            string dbName = WebConfigurationManager.AppSettings["dbname"];
            string user = WebConfigurationManager.AppSettings["dbuser"];
            string password = WebConfigurationManager.AppSettings["dbPassword"];
            string port = WebConfigurationManager.AppSettings["dbPort"];
            ConnectionFactory.setConfiguration(host, dbName, port, user, password);
        }
    }
}