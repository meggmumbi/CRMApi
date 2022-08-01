using System;
using System.Configuration;
using System.Net;
using cicapi.NavOData;

namespace cicapi.Utils
{
    public class DBConfig
    {
        public static NAV ReturnNav(string url)
        {
            NAV nav = new NAV(new Uri(url));
            nav.Credentials = (ICredentials)new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
            return nav;
        }



        public Mobile.CRMIntegration ObjNav(string url)
        {
            Mobile.CRMIntegration mobile = new Mobile.CRMIntegration(url);
            try
            {
                NetworkCredential networkCredential = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"], ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                mobile.Credentials = (ICredentials)networkCredential;
                mobile.PreAuthenticate = true;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return mobile;
        }

    }
}