using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using PS_TUP;

namespace PS_TUP.Controllers.MensajeroEntity
{
    public class MensajesController : Controller
    {
        private PS_TUPMensajeroEntities db = new PS_TUPMensajeroEntities();
        private PS_TUPEntitiesGestionUsuarios dbDenuncias = new PS_TUPEntitiesGestionUsuarios();

        // GET: Mensajes
        public ActionResult Index()
        {
            return View(db.Mensajes.ToList());
        }

        // GET: Mensajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajes mensajes = db.Mensajes.Find(id);

            //var mensajes = (from u in db.Mensajes
            //                 where u.UserName == User.Identity.Name
            //                 where u.IdDenuncia == id
            //                 select u).FirstOrDefault();

            if (mensajes == null)
            {
                return HttpNotFound();
            }
            return View(mensajes);
        }

        // GET: Mensajes/Create
        [Authorize(Roles = "Usuario")]
        public ActionResult Create(int id)
        {
            var denuncia = dbDenuncias.Denuncias.Where(t => t.IdDenuncia == id).FirstOrDefault();

            if (denuncia.UserName == User.Identity.Name)
            {
                var mensajes = db.Mensajes.Where(t => t.IdDenuncia == id).ToList();
                //var mensajes = (from u in db.Mensajes
                //                   where u.UserName == User.Identity.Name
                //                   where u.IdDenuncia == id
                //                   select u).ToList();

                ViewBag.items = mensajes;

                var denuncias = dbDenuncias.Denuncias.Where(t => t.IdDenuncia == id).ToList();
                ViewBag.denuncias = denuncias;

                return View();
            }

            else
            {
                return HttpNotFound();
            }


        }

        // POST: Mensajes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMensaje,IdDenuncia,UserName,Fecha,Mensaje,Imagen1,Archivo,Role")] Mensajes mensajes, int id)
        {
            var mensajes1 = db.Mensajes.Where(t => t.IdDenuncia == id).ToList();
            ViewBag.items = mensajes1;

            var denuncias = dbDenuncias.Denuncias.Where(t => t.IdDenuncia == id).ToList();
            ViewBag.denuncias = denuncias;

            if (ModelState.IsValid)
            {
                Mensajes m = new Mensajes();
                m.IdDenuncia = id;
                m.UserName = User.Identity.Name;
                m.Fecha = System.DateTime.Now;
                m.Mensaje = mensajes.Mensaje;          
                m.Imagen1 = mensajes.Imagen1;
                m.Archivo = mensajes.Archivo;
                m.Role = "Usuario";

                db.Mensajes.Add(m);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(mensajes);
        }

        ///////////////////////////////////////////////////////////////
        ///DESDE FISCALIA///
        [Authorize(Roles = "Fiscalia")]
        public ActionResult CreateDesdeFiscalia(int id)
        {
            var mensajes = db.Mensajes.Where(t => t.IdDenuncia == id).ToList();
            ViewBag.items = mensajes;

            var denuncias = dbDenuncias.Denuncias.Where(t => t.IdDenuncia == id).ToList();
            ViewBag.denuncias = denuncias;
            return View();
        }

        // POST: Mensajes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDesdeFiscalia([Bind(Include = "IdMensaje,IdDenuncia,UserName,Fecha,Mensaje,Imagen1,Archivo,Role")] Mensajes mensajes, int id)
        {
            var mensajes1 = db.Mensajes.Where(t => t.IdDenuncia == id).ToList();
            ViewBag.items = mensajes1;

            var denuncias = dbDenuncias.Denuncias.Where(t => t.IdDenuncia == id).ToList();
            ViewBag.denuncias = denuncias;

            PS_TUPEntities dbUser = new PS_TUPEntities();

           

            if (ModelState.IsValid)
            {
               
                Mensajes m = new Mensajes();
                m.IdDenuncia = id;
                m.UserName = User.Identity.Name;
                m.Fecha = System.DateTime.Now;
                m.Mensaje = mensajes.Mensaje;
                m.Imagen1 = mensajes.Imagen1;
                m.Archivo = mensajes.Archivo;
                m.Role = "Fiscalia";

                var denuncia = (from u in dbDenuncias.Denuncias
                            where u.IdDenuncia == m.IdDenuncia
                            select u).FirstOrDefault();

                var mail = (from u in dbUser.Usuarios
                                where u.UserName == denuncia.UserName
                                select u.Email).FirstOrDefault();

                //var mail = dbDenuncias.Denuncias.Where(t => t. == id).ToList();

                string email = mail.ToString();
                string mensaje = m.Mensaje;

                db.Mensajes.Add(m);
                db.SaveChanges();
                enviarEMail(email, mensaje);
                return RedirectToAction("CreateDesdeFiscalia");
            }

            return View(mensajes);
        }


        public void enviarEMail(string correo, string mensaje)
        {
            MailMessage Correo = new MailMessage();
            SmtpClient ServerEmail = new SmtpClient();
            ServerEmail.Credentials = new NetworkCredential("*Solicitar a admin*", "*Solicitar a admin*");
            ServerEmail.Host = "smtp.gmail.com";
            ServerEmail.Port = 587;
            ServerEmail.EnableSsl = true;


            if (IsValidEmail(correo))
            {
                Correo.From = new MailAddress("*Solicitar a admin*");
                Correo.To.Add(correo);
                Correo.Subject = ("Nuevo Mensaje en tu Denuncia - DenunciasOnline");
                Correo.Body = "Hola! Fiscalía te envió un mensaje: " + mensaje;
                Correo.Priority = MailPriority.Normal;
                ServerEmail.Send(Correo);
                Correo.Dispose();
            }

 
            
        }

        ////////////////////////////////////////////////////////////////////////////
        // GET: Mensajes/Create
        [Authorize(Roles = "Fiscalia")]
        public ActionResult MensajesDenunciasCerradas(int id)
        {
            var mensajes = db.Mensajes.Where(t => t.IdDenuncia == id).ToList();
            ViewBag.items = mensajes;

            var denuncias = dbDenuncias.Denuncias.Where(t => t.IdDenuncia == id).ToList();
            ViewBag.denuncias = denuncias;
            return View();
        }

        [Authorize(Roles = "Usuario")]
        public ActionResult MensajesDenunciasCerradasVistaUsuario(int id)
        {

            var denuncia = dbDenuncias.Denuncias.Where(t => t.IdDenuncia == id).FirstOrDefault();

            if (denuncia.UserName == User.Identity.Name)
            {
                var mensajes = db.Mensajes.Where(t => t.IdDenuncia == id).ToList();
                //var mensajes = (from u in db.Mensajes
                //                   where u.UserName == User.Identity.Name
                //                   where u.IdDenuncia == id
                //                   select u).ToList();

                ViewBag.items = mensajes;

                var denuncias = dbDenuncias.Denuncias.Where(t => t.IdDenuncia == id).ToList();
                ViewBag.denuncias = denuncias;

                return View();
            }

            else
            {
                return HttpNotFound();
            }


            //var mensajes = db.Mensajes.Where(t => t.IdDenuncia == id).ToList();
            //ViewBag.items = mensajes;

            //var denuncias = dbDenuncias.Denuncias.Where(t => t.IdDenuncia == id).ToList();
            //ViewBag.denuncias = denuncias;
            //return View();
        }


        // GET: Mensajes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajes mensajes = db.Mensajes.Find(id);
            if (mensajes == null)
            {
                return HttpNotFound();
            }
            return View(mensajes);
        }

        // POST: Mensajes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMensaje,IdDenuncia,UserName,Fecha,Mensaje,Imagen1,Archivo,Role")] Mensajes mensajes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensajes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mensajes);
        }

        // GET: Mensajes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajes mensajes = db.Mensajes.Find(id);
            if (mensajes == null)
            {
                return HttpNotFound();
            }
            return View(mensajes);
        }

        // POST: Mensajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mensajes mensajes = db.Mensajes.Find(id);
            db.Mensajes.Remove(mensajes);
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



        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                Console.Write(e);
                return false;
            }
            catch (ArgumentException e)
            {
                Console.Write(e);
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
