using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PS_TUP;

namespace PS_TUP.Controllers.GestionEmpresas
{
    [Authorize(Roles = "Empresa")]
    public class RedesSocEmpresasController : Controller
    {
        private PS_TUPEntitiesGestionEmpresas db = new PS_TUPEntitiesGestionEmpresas();

        // GET: RedesSocEmpresas
        public ActionResult Index()
        {
            var listado = db.RedesSocEmpresas.Where(t => t.UserName == User.Identity.Name).ToList();
            return View(listado);
        }

        // GET: RedesSocEmpresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RedesSocEmpresas redesSocEmpresas = db.RedesSocEmpresas.Find(id);
            if (redesSocEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(redesSocEmpresas);
        }

        // GET: RedesSocEmpresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RedesSocEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRegRed,UserName,Red,Link,Descripcion")] RedesSocEmpresas redesSocEmpresas)
        {
            if (ModelState.IsValid)
            {
                RedesSocEmpresas red = new RedesSocEmpresas();
                red.Red = redesSocEmpresas.Red;
                red.Link = redesSocEmpresas.Link;
                red.Descripcion = redesSocEmpresas.Descripcion;
                red.UserName = User.Identity.Name;

                db.RedesSocEmpresas.Add(red);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(redesSocEmpresas);
        }

        // GET: RedesSocEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RedesSocEmpresas redesSocEmpresas = db.RedesSocEmpresas.Find(id);
            if (redesSocEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(redesSocEmpresas);
        }

        // POST: RedesSocEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRegRed,UserName,Red,Link,Descripcion")] RedesSocEmpresas redesSocEmpresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(redesSocEmpresas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(redesSocEmpresas);
        }

        // GET: RedesSocEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RedesSocEmpresas redesSocEmpresas = db.RedesSocEmpresas.Find(id);
            if (redesSocEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(redesSocEmpresas);
        }

        // POST: RedesSocEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RedesSocEmpresas redesSocEmpresas = db.RedesSocEmpresas.Find(id);
            db.RedesSocEmpresas.Remove(redesSocEmpresas);
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
