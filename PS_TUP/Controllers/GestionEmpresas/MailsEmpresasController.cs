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
    public class MailsEmpresasController : Controller
    {
        private PS_TUPEntitiesGestionEmpresas db = new PS_TUPEntitiesGestionEmpresas();

        // GET: MailsEmpresas
        public ActionResult Index()
        {
            var listado = db.MailsEmpresas.Where(t => t.UserName == User.Identity.Name).ToList();
            return View(listado);
        }

        // GET: MailsEmpresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailsEmpresas mailsEmpresas = db.MailsEmpresas.Find(id);
            if (mailsEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(mailsEmpresas);
        }

        // GET: MailsEmpresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MailsEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMailEmpresa,UserName,Descripcion,Mail")] MailsEmpresas mailsEmpresas)
        {
            if (ModelState.IsValid)
            {
                MailsEmpresas mail = new MailsEmpresas();
                mail.Descripcion = mailsEmpresas.Descripcion;
                mail.Mail = mailsEmpresas.Mail;
                mail.UserName = User.Identity.Name;

                db.MailsEmpresas.Add(mail);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mailsEmpresas);
        }

        // GET: MailsEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailsEmpresas mailsEmpresas = db.MailsEmpresas.Find(id);
            if (mailsEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(mailsEmpresas);
        }

        // POST: MailsEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMailEmpresa,UserName,Descripcion,Mail")] MailsEmpresas mailsEmpresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mailsEmpresas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mailsEmpresas);
        }

        // GET: MailsEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MailsEmpresas mailsEmpresas = db.MailsEmpresas.Find(id);
            if (mailsEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(mailsEmpresas);
        }

        // POST: MailsEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MailsEmpresas mailsEmpresas = db.MailsEmpresas.Find(id);
            db.MailsEmpresas.Remove(mailsEmpresas);
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
