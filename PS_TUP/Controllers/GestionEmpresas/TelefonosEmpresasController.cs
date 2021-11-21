using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PS_TUP;

namespace PS_TUP.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class TelefonosEmpresasController : Controller
    {
        private PS_TUPEntitiesGestionEmpresas db = new PS_TUPEntitiesGestionEmpresas();

        // GET: TelefonosEmpresas
        public ActionResult Index()
        {
          
            var listado = db.TelefonosEmpresas.Where(t => t.UserName2 == User.Identity.Name).ToList();
            return View(listado);
        }

        // GET: TelefonosEmpresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelefonosEmpresas telefonosEmpresas = db.TelefonosEmpresas.Find(id);
            if (telefonosEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(telefonosEmpresas);
        }

        // GET: TelefonosEmpresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TelefonosEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRegTelefono,UserName2,Descripcion,Numero")] TelefonosEmpresas telefonosEmpresas)
        {
            if (ModelState.IsValid)
            {
                TelefonosEmpresas tel= new TelefonosEmpresas();
                tel.Descripcion = telefonosEmpresas.Descripcion;
                tel.Numero = telefonosEmpresas.Numero;
                tel.UserName2 = User.Identity.Name;

                db.TelefonosEmpresas.Add(tel);
                //db.TelefonosEmpresas.Add(telefonosEmpresas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(telefonosEmpresas);
        }

        // GET: TelefonosEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelefonosEmpresas telefonosEmpresas = db.TelefonosEmpresas.Find(id);
            if (telefonosEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(telefonosEmpresas);
        }

        // POST: TelefonosEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRegTelefono,UserName2,Descripcion,Numero")] TelefonosEmpresas telefonosEmpresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefonosEmpresas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(telefonosEmpresas);
        }

        // GET: TelefonosEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelefonosEmpresas telefonosEmpresas = db.TelefonosEmpresas.Find(id);
            if (telefonosEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(telefonosEmpresas);
        }

        // POST: TelefonosEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TelefonosEmpresas telefonosEmpresas = db.TelefonosEmpresas.Find(id);
            db.TelefonosEmpresas.Remove(telefonosEmpresas);
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
