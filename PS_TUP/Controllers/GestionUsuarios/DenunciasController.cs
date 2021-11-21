using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Helpers;
using PS_TUP;
using System.Net.Mail;
using PS_TUP.Models.DTO;
using PS_TUP.AccesoDatos;
using PS_TUP.Models;
using Rotativa;
using System.Text.RegularExpressions;
using System.Globalization;
using PS_TUP.Models.TiposUsuario;

namespace PS_TUP.Controllers.GestionUsuarios
{
    [Authorize(Roles = "Usuario, Fiscalia")]
    public class DenunciasController : Controller
    {
        private PS_TUPEntitiesGestionUsuarios db = new PS_TUPEntitiesGestionUsuarios();

        // GET: Denuncias
        public ActionResult Index()
        {

            var denuncias = (from u in db.Denuncias
                        where u.UserName == User.Identity.Name
                        where u.activa == "Abierta"
                        select u).ToList();

            var listado = db.Denuncias.Where(t => t.UserName == User.Identity.Name).ToList();       
            return View(denuncias);
        }

        public ActionResult DenunciasCerradasVistaUsuario()
        {

            var denuncias = (from u in db.Denuncias
                             where u.UserName == User.Identity.Name
                             where u.activa == "Cerrada"
                             select u).ToList();

            var listado = db.Denuncias.Where(t => t.UserName == User.Identity.Name).ToList();
            return View(denuncias);
        }

        // GET: Denuncias/Details/5
       // [Route("Detalles/Denuncia")]
        public ActionResult Details(int? id)
        {

      

           // var listado = db.Denuncias.Where(t => t.UserName == User.Identity.Name).ToList();

            if (User.Identity.Name == User.Identity.Name)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //Denuncias denuncias = db.Denuncias.Find(id);

                var denuncias = (from u in db.Denuncias
                                where u.UserName == User.Identity.Name
                                where u.IdDenuncia == id
                                where u.activa == "Abierta"
                                select u).FirstOrDefault();

                if (denuncias == null)
                {
                    return HttpNotFound();
                }
                return View(denuncias);
            }
            else
            {
                return HttpNotFound();
            }

         
        }

        public ActionResult DetallesDenunciasCerradas(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Denuncias denuncias = db.Denuncias.Find(id);

            var denuncias = (from u in db.Denuncias
                             where u.UserName == User.Identity.Name
                             where u.IdDenuncia == id
                             where u.activa == "Cerrada"
                             select u).FirstOrDefault();

            if (denuncias == null)
            {
                return HttpNotFound();
            }
            return View(denuncias);
        }


        // GET: Denuncias/Create
        public ActionResult Create()
        {
            RegistroTipoUsuario resultado = GestorBD.ObtenerUsuario(User.Identity.Name);

            List<MotivosDenuncias> listaMotivos = GestorBD.ObtenerMotivosDenuncias();
            List<SelectListItem> itemsCombo = listaMotivos.ConvertAll(m =>
            {
                return new SelectListItem()
                {
                    Text = m.DescripcionMotivo,
                    Value = m.DescripcionMotivo,
                    Selected = false 
                };
            });

            ViewBag.items = itemsCombo;
            ViewBag.tienePerfilActualizado = resultado.datosActualizados;

            return View();
        }

        // POST: Denuncias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDenuncia,IdTipoDenuncia,Descripcion,Foto1,Foto2,Archivo,Fiscalia,UserName,Fecha,activa,Audio1,Audio2")] Denuncias denuncias)
        {

            PS_TUPEntities dbUser = new PS_TUPEntities();

            var mail = (from u in dbUser.Usuarios
                        where u.UserName == User.Identity.Name
                        select u.Email).FirstOrDefault();    
            string email = mail.ToString();

            List<MotivosDenuncias> listaMotivos = GestorBD.ObtenerMotivosDenuncias();
            List<SelectListItem> itemsCombo = listaMotivos.ConvertAll(m =>
            {
                return new SelectListItem()
                {
                    Text = m.DescripcionMotivo,
                    Value = m.DescripcionMotivo,
                    Selected = false 
                };
            });

            foreach (var item in itemsCombo)
            {
                if (item.Value.Equals(denuncias.Descripcion))
                {
                    item.Selected = true;
                    break;
                }
            }

            ViewBag.items = itemsCombo;


            HttpPostedFileBase FileBase1 = Request.Files[0];
            byte[] image1 = new byte[FileBase1.ContentLength];
            FileBase1.InputStream.Read(image1, 0, FileBase1.ContentLength);
            //WebImage image1 = new WebImage(FileBase1.InputStream);
            //denuncias.Foto1 = image1.GetBytes();

            HttpPostedFileBase FileBase2 = Request.Files[1];
            byte[] image2 = new byte[FileBase2.ContentLength];
            FileBase2.InputStream.Read(image2, 0, FileBase2.ContentLength);
            //WebImage image2 = new WebImage(FileBase2.InputStream);
            //denuncias.Foto2 = image2.GetBytes();

            HttpPostedFileBase FileBase3 = Request.Files[2];
            byte[] Data = new byte[FileBase3.ContentLength];
            FileBase3.InputStream.Read(Data, 0, FileBase3.ContentLength);
            denuncias.Archivo = Data;


            HttpPostedFileBase FileBase4 = Request.Files[3];
            byte[] audio1 = new byte[FileBase4.ContentLength];
            FileBase4.InputStream.Read(audio1, 0, FileBase4.ContentLength);
            denuncias.Audio1 = audio1;


            HttpPostedFileBase FileBase5 = Request.Files[4];
            byte[] audio2 = new byte[FileBase5.ContentLength];
            FileBase5.InputStream.Read(audio2, 0, FileBase5.ContentLength);
            denuncias.Audio2 = audio2;

            if (ModelState.IsValid)
            {
                Denuncias denuncia = new Denuncias();
                denuncia.IdTipoDenuncia = denuncias.IdTipoDenuncia;
                denuncia.Descripcion = denuncias.Descripcion;
                denuncia.Foto1 = image1; // si pongo denuncia.Foto1 = denuncias.Foto1;  --la imagen queda seteada en NULL EN LA BD!!!! TENER EN CUENTA
                denuncia.Foto2 = image2;
                denuncia.Archivo = Data;
                denuncia.Fiscalia = denuncias.Fiscalia;
                denuncia.Fecha = DateTime.Now;
                denuncia.activa = "Abierta";
                denuncia.Audio1 = audio1;
                denuncia.Audio2 = audio2;

                denuncia.UserName = User.Identity.Name;

                db.Denuncias.Add(denuncia);           
                db.SaveChanges();

                enviarEMail(email);  /*REACTIVAR!!!!*/

                return RedirectToAction ("DenunciaRecibidaDescargarPDF");
            }

            return View(denuncias);
        }


        public void enviarEMail(string correo)
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
                Correo.Subject = ("Nueva Denuncia Registrada - DenunciasOnline");
                Correo.Body = "Hola! El equipo de DenunciasOnline le confirma la recepción de su denuncia. Le responderemos a la brevedad. ";
                Correo.Priority = MailPriority.Normal;
                ServerEmail.Send(Correo);
                Correo.Dispose();
            }


        }

        //////////////////////////////////////////////////////////////////////////////////////

        public FileContentResult Download(int id)
        {
            byte[] Data;
            Denuncias denuncias = db.Denuncias.Find(id);
            Data = (byte[])denuncias.Foto1.ToArray();
            return File(Data, "Text");
        }


        public FileContentResult Download2(int id)
        {
            byte[] Data;
            Denuncias denuncias = db.Denuncias.Find(id);
            Data = (byte[])denuncias.Foto2.ToArray();
            return File(Data, "Text");
        }


        public FileContentResult Download3(int id)
        {
            byte[] Data;
            Denuncias denuncias = db.Denuncias.Find(id);
            Data = (byte[])denuncias.Archivo.ToArray();
           // stream, "application/pdf", "DownloadName.pdf"

            return File(Data, "application/pdf", "Documento.pdf");
        }

      
        public FileContentResult Download4(int id)
        {
            byte[] Data;
            Denuncias denuncias = db.Denuncias.Find(id);
            Data = (byte[])denuncias.Audio1.ToArray();

            return File(Data, "application/mp3", "Audio1.mp3");
        }


        public FileContentResult Download5(int id)
        {
            byte[] Data;
            Denuncias denuncias = db.Denuncias.Find(id);
            Data = (byte[])denuncias.Audio2.ToArray();

            return File(Data, "application/mp3", "Audio2.mp3");
        }


        //////////////////////////////////////////////////////////////////////////////////////


        public ActionResult DenunciaRecibidaDescargarPDF()
        {
            return View();
        }

        public ActionResult PrintReporteDenuncia(/*string motivo, string descripcion*/)
        {
            var denuncia = (from u in db.Denuncias
                            where u.UserName == User.Identity.Name
                            orderby u.Fecha descending
                            select u).FirstOrDefault();

            var user = (from u in db.tipoUsuario
                               where u.UserName == User.Identity.Name                           
                               select u).FirstOrDefault();

            string denunciante = user.nombre + " " +user.apellido;
            string motivo = denuncia.IdTipoDenuncia;
            string descripcion = denuncia.Descripcion;
            DateTime fecha = denuncia.Fecha;
            string idDenuncia = "000" + denuncia.IdDenuncia;


            return new ActionAsPdf("ReporteDenuncia", new { denunciante, motivo, descripcion, fecha, idDenuncia })
            { FileName = "Denuncia.pdf" };
        }

        

        public ActionResult ReporteDenuncia(string denunciante, string motivo, string descripcion, DateTime fecha, string idDenuncia)
        {
            ViewBag.denunciante = denunciante;
            ViewBag.motivo = motivo;
            ViewBag.descripcion = descripcion;
            ViewBag.fecha = fecha;
            ViewBag.idDenuncia = idDenuncia;
            return View();
        }

        //////////////////////////////////////////////////////////////////////////////////////
        
        // GET: Denuncias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Denuncias denuncias = db.Denuncias.Find(id);
            if (denuncias == null)
            {
                return HttpNotFound();
            }
            return View(denuncias);
        }

        // POST: Denuncias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDenuncia,IdTipoDenuncia,Descripcion,Foto1,Foto2,Archivo,Fiscalia,UserName,Fecha,activa")] Denuncias denuncias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(denuncias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(denuncias);
        }

        // GET: Denuncias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Denuncias denuncias = db.Denuncias.Find(id);
            if (denuncias == null)
            {
                return HttpNotFound();
            }
            return View(denuncias);
        }

        // POST: Denuncias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Denuncias denuncias = db.Denuncias.Find(id);
            db.Denuncias.Remove(denuncias);
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
