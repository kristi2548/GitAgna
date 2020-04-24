using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using ApiWithAuthentication.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Data;
using System.Data.Entity;
using System.Web;
//using System.Web.Mvc;

namespace ApiWithAuthentication.Controllers
{
    public class TestController : ApiController
    {
        private AGNAPPCORDERSEntities db = new AGNAPPCORDERSEntities();

        [BasicAuthentication]
        [Route("api/Users/GetEmployees/{UserId}")]
        public HttpResponseMessage GetEmployees()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            db.Configuration.ProxyCreationEnabled = false;

            List<A_Qytete> EntitetEksport = new List<A_Qytete>();
            using (AGNAPPCORDERSEntities myCustomersDb = new Models.AGNAPPCORDERSEntities())
            {
                EntitetEksport = myCustomersDb.A_Qytete.OrderBy(a => a.QNM).ToList();
            }
            System.Web.Mvc.JsonResult jsonMe = new System.Web.Mvc.JsonResult { Data = EntitetEksport, JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet };

            switch (username.ToLower())
            {
                case "agna":
                    return Request.CreateResponse(HttpStatusCode.OK, jsonMe);
                        ////db.A_Qytete.Select(m => new
                        ////{
                        ////    Qyteti = m.QNM,
                        ////    Qid = m.QID,
                        ////    QytetKod = m.QCD
                        ////}));
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

    }
}
