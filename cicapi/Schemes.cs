using cicapi.NavOData;
using cicapi.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace cicapi
{
    public class Schemes
    {
        public string SchemeName { get; set; }

        public string SchemeODataUrl { get; set; }

        public string SchemeWsUrl { get; set; }

        public Schemes(string name)
        {
            DataServiceQuery<CompaniesCRM> companies1 = DBConfig.ReturnNav(ConfigurationManager.AppSettings["ODATA_URI"]).CompaniesCRM;
            Expression<Func<CompaniesCRM, bool>> predicate = (Expression<Func<CompaniesCRM, bool>>)(r => r.Name == name);
            foreach (CompaniesCRM companies2 in (IEnumerable<CompaniesCRM>)companies1.Where<CompaniesCRM>(predicate))
            {
                this.SchemeName = companies2.Name;
                this.SchemeODataUrl = companies2.oDataUrl;
                this.SchemeWsUrl = companies2.WS_URL;
            }
        }

        public Schemes()
        {
        }
    }
}