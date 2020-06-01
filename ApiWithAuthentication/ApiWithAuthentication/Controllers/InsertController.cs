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
using System.Web.Mvc;
namespace ApiWithAuthentication.Controllers
{
    public partial class Welcome
    {
        [JsonProperty("QytetiObjS")]
        public QytetiObj[] qytete { get; set; }
    }
    public partial class QytetiObj
    {
        [JsonProperty("qyteti")]
        public string Qyteti { get; set; }

        [JsonProperty("qid")]
        public string Qid { get; set; }

        [JsonProperty("qytetKod")]
        public string QytetKod { get; set; }
    }
    public class InsertController : ApiController
    {
        private AGNAPPCORDERSEntities db = new AGNAPPCORDERSEntities();

        [BasicAuthentication]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage insertEmployee()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            Global.localConn =
             System.Configuration.ConfigurationManager.ConnectionStrings["localConnection"].ConnectionString;
            switch (username.ToLower())
            {
                case "agna":
                    var client = new RestClient("http://localhost:19624/api/test/getemployees");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Authorization", "Basic YWduYTprb3JjYXJp");
                    IRestResponse response = client.Execute(request);
                    Console.WriteLine(response.Content);

                    var JSONObj = JsonConvert.DeserializeObject<Welcome>(response.ToString()); //convert you json in to class object
                    QytetiObj oMyclass = Newtonsoft.Json.JsonConvert.DeserializeObject<QytetiObj>(response.ToString());
                    //var EmpArray = JSONObj["QytetiObjS"]; // will give you employee array
                    string insert = "Insert into A_Qytete qid,qnm,qcd  values ( '" + oMyclass.Qid + "'," + oMyclass.Qyteti + "'," + oMyclass.QytetKod + "' )";
                    Global.callSqlCommand(Global.localConn, insert, "Text", "NonDataTable");
                    return null;
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return null;
        }
        [BasicAuthentication]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetEmployeesNew()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            db.Configuration.ProxyCreationEnabled = false;

            List<A_Qytete> EntitetEksport = new List<A_Qytete>();
            using (AGNAPPCORDERSEntities myCustomersDb = new AGNAPPCORDERSEntities())
            {
                EntitetEksport = myCustomersDb.A_Qytete.OrderBy(a => a.QNM).ToList();
            }
            JsonResult jsonMe = new JsonResult { Data = EntitetEksport, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

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
