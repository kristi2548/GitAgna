using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ApiWithAuthentication.Models;
using System.Threading;

namespace ApiWithAuthentication.Controllers
{
    public class A_QyteteController : Controller
    {
        private AGNAPPCORDERSEntities db = new AGNAPPCORDERSEntities();
        private A_Qytete _context;
        [BasicAuthentication]
        public HttpResponseMessage GetEmployees(A_Qytete context)
        {
            //var userStore = new UserStore<IdentityUser>();
            string username = Thread.CurrentPrincipal.Identity.Name;

            _context = context;

            //var notFoundResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
            //throw new HttpResponseException(notFoundResponse);
            //HttpRequestMessage request = new HttpRequestMessage();
            //var response = request.CreateResponse(HttpStatusCode.OK, _context.QNM);

            switch (username.ToLower())
            {
                case "agna":
                    HttpRequestMessage request = new HttpRequestMessage();
                    var response = request.CreateResponse(HttpStatusCode.OK, _context.QNM);
                    return response;
                        //Request.CreateResponse(HttpStatusCode.OK,
                        //db.A_Qytete.Select(m => new
                        //{
                        //    ProductNav = m.QNM,
                        //    ProductWebName = m.QID,
                        //    ItemPrice = m.QCD
                        //}));
                default:
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                    //Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
       
        

        [BasicAuthentication]
        public ActionResult getQytete()
        {
            //return View(db.ProdukteApis.ToList());
            string username = Thread.CurrentPrincipal.Identity.Name;
            try
            {
                List<A_Qytete> EntitetEksport = new List<A_Qytete>();
                using (AGNAPPCORDERSEntities myCustomersDb = new AGNAPPCORDERSEntities())
                {
                    EntitetEksport = myCustomersDb.A_Qytete.OrderBy(a => a.QNM).ToList();
                }
                return new JsonResult { Data = EntitetEksport, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                Console.Write("Gabim " + ex.Message);
                return null;
            }
        }
        [BasicAuthentication]
        //public HttpResponseMessage GetEmployees()
        //{

        //    string username = Thread.CurrentPrincipal.Identity.Name;

        //    switch (username.ToLower())
        //    {
        //        case "agna":
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //                db.wProducts.Select(m => new
        //                {
        //                    ProductNav = m.ProductNav,
        //                    ProductWebName = m.ProductWebNameAL,
        //                    ItemPrice = m.ProductPrice,
        //                    UnitsPack = m.UnitsPack,
        //                    ProductNotes = m.ProductNotes,
        //                    ProductTS = m.ProductTS
        //                }));
        //        default:
        //            return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //}
        public ActionResult Index()
        {
            return View(db.A_Qytete.ToList());
        }

        // GET: A_Qytete/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            A_Qytete a_Qytete = db.A_Qytete.Find(id);
            if (a_Qytete == null)
            {
                return HttpNotFound();
            }
            return View(a_Qytete);
        }

        // GET: A_Qytete/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: A_Qytete/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QID,PID,SID,QCD,QNM,RPSTG,RPSTP")] A_Qytete a_Qytete)
        {
            if (ModelState.IsValid)
            {
                db.A_Qytete.Add(a_Qytete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(a_Qytete);
        }

        // GET: A_Qytete/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            A_Qytete a_Qytete = db.A_Qytete.Find(id);
            if (a_Qytete == null)
            {
                return HttpNotFound();
            }
            return View(a_Qytete);
        }

        // POST: A_Qytete/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QID,PID,SID,QCD,QNM,RPSTG,RPSTP")] A_Qytete a_Qytete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(a_Qytete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(a_Qytete);
        }

        // GET: A_Qytete/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            A_Qytete a_Qytete = db.A_Qytete.Find(id);
            if (a_Qytete == null)
            {
                return HttpNotFound();
            }
            return View(a_Qytete);
        }

        // POST: A_Qytete/Delete/5
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            A_Qytete a_Qytete = db.A_Qytete.Find(id);
            db.A_Qytete.Remove(a_Qytete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
