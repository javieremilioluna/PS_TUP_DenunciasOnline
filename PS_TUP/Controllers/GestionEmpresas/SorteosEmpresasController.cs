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
    public class SorteosEmpresasController : Controller
    {
        private PS_TUPEntitiesGestionEmpresas db = new PS_TUPEntitiesGestionEmpresas();

        // GET: SorteosEmpresas
        public ActionResult Index()
        {
            var listado = db.SorteosEmpresas.Where(t => t.UserName == User.Identity.Name).ToList();
            return View(listado);
        }

        // GET: SorteosEmpresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //SorteosEmpresas sorteosEmpresas = db.SorteosEmpresas.Find(id);

            var sorteosEmpresas = (from u in db.SorteosEmpresas
                             where u.UserName == User.Identity.Name
                             where u.IdSorteo == id
                             select u).FirstOrDefault();

            if (sorteosEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(sorteosEmpresas);
        }

        // GET: SorteosEmpresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SorteosEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSorteo,UserName,Titulo,Descripcion,FechaInicio,FechaFin,Ganadores,Activo")] SorteosEmpresas sorteosEmpresas)
        {
            if (ModelState.IsValid)
            {
                SorteosEmpresas sorteo = new SorteosEmpresas();
                sorteo.Titulo = sorteosEmpresas.Titulo;
                sorteo.Descripcion = sorteosEmpresas.Descripcion;
                sorteo.FechaInicio = sorteosEmpresas.FechaInicio;
                sorteo.FechaFin = sorteosEmpresas.FechaFin;
                sorteo.Ganadores = sorteosEmpresas.Ganadores;
                sorteo.Activo = sorteosEmpresas.Activo;
                sorteo.UserName = User.Identity.Name;

                db.SorteosEmpresas.Add(sorteo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sorteosEmpresas);
        }

        // GET: SorteosEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // SorteosEmpresas sorteosEmpresas = db.SorteosEmpresas.Find(id);
            var sorteosEmpresas = (from u in db.SorteosEmpresas
                                   where u.UserName == User.Identity.Name
                                   where u.IdSorteo == id
                                   select u).FirstOrDefault();

            if (sorteosEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(sorteosEmpresas);
        }

        // POST: SorteosEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSorteo,UserName,Titulo,Descripcion,FechaInicio,FechaFin,Ganadores,Activo")] SorteosEmpresas sorteosEmpresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sorteosEmpresas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sorteosEmpresas);
        }

        // GET: SorteosEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //SorteosEmpresas sorteosEmpresas = db.SorteosEmpresas.Find(id);
            var sorteosEmpresas = (from u in db.SorteosEmpresas
                                   where u.UserName == User.Identity.Name
                                   where u.IdSorteo == id
                                   select u).FirstOrDefault();
            if (sorteosEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(sorteosEmpresas);
        }

        // POST: SorteosEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SorteosEmpresas sorteosEmpresas = db.SorteosEmpresas.Find(id);
            db.SorteosEmpresas.Remove(sorteosEmpresas);
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
