using PS_TUP.AccesoDatos;
using PS_TUP.Models.TipoEmpresa;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using PS_TUP.Models.DTO;
using System.Net.Mail;
using System.Net;

namespace PS_TUP.Controllers
{
    public class HomeController : Controller
    {
        private PS_TUPEntitiesGestionEmpresas db = new PS_TUPEntitiesGestionEmpresas();

        public ActionResult Index()
        {       
            return View();
        }

        public ActionResult ResultadosBusqueda(string nombreComercial)
        {
            List<Empresas> listado = GestorBDBuscadorListadoEmpresas.ObtenerEmpresasBuscador(nombreComercial);
            return View(listado);
        }


        public ActionResult InfoDenuncias()
        {
            return View();
        }

        public ActionResult Ayuda()
        {
            return View();
        }



        public ActionResult Empresas(string search,int? i)
        {
            List<Empresas> listado = GestorBDBuscadorListadoEmpresas.ObtenerEmpresasListado();
            return View(listado.ToPagedList(i ?? 1,3));
        }

        public ActionResult getImage(string UserName)
        {
            RegistroTipoEmpresa resultado = GestorBD.ObtenerEmpresa(UserName);
            byte[] byteImage = resultado.logo;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }


        public ActionResult DetalleEmpresa(int idEmpresa)
        {
            Empresas empresa = GestorBDBuscadorListadoEmpresas.ObtenerEmpresa(idEmpresa);     
            return View(empresa);
        }      
       
        ////////////////////////////////////////////////////////////////////

        public ActionResult TelefonosEmpresa(string UserName, int idEmpresa)
        {
            var listado = db.TelefonosEmpresas.Where(t => t.UserName2 == UserName).ToList();

            Empresas empresa = GestorBDBuscadorListadoEmpresas.ObtenerEmpresa(idEmpresa);
            ViewBag.empresa = empresa;

            return View(listado);         
        }

        public ActionResult MailsEmpresa(string UserName, int idEmpresa)
        {
            var listado = db.MailsEmpresas.Where(t => t.UserName == UserName).ToList();

            Empresas empresa = GestorBDBuscadorListadoEmpresas.ObtenerEmpresa(idEmpresa);
            ViewBag.empresa = empresa;

            return View(listado);          
        }

        public ActionResult RedesSociales(string UserName, int idEmpresa)
        {
            var listado = db.RedesSocEmpresas.Where(t => t.UserName == UserName).ToList();

            Empresas empresa = GestorBDBuscadorListadoEmpresas.ObtenerEmpresa(idEmpresa);
            ViewBag.empresa = empresa;

            return View(listado);

        }

        public ActionResult SorteosEmpresa(string UserName, int idEmpresa)
        {
            var listado = db.SorteosEmpresas.Where(t => t.UserName == UserName).ToList();

            Empresas empresa = GestorBDBuscadorListadoEmpresas.ObtenerEmpresa(idEmpresa);
            ViewBag.empresa = empresa;

            return View(listado);
        }

        public ActionResult GanadoresSorteo( int idSorteo, int idEmpresa)
        {
            var sorteo = db.SorteosEmpresas.Where(t => t.IdSorteo == idSorteo).FirstOrDefault();

            Empresas empresa = GestorBDBuscadorListadoEmpresas.ObtenerEmpresa(idEmpresa);
            ViewBag.empresa = empresa;

          //  if (empresa is null)

            return View(sorteo);
        }

        ///////////////////////////////////////////////////////////////

        public ActionResult PruebaModal()
        {
            return PartialView("PruebaModal");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public void mailContacto(MensajeContacto modelo)
        {

            MailMessage Correo = new MailMessage();
            string correo = "*Solicitar a admin*";
            Correo.From = new MailAddress("*Solicitar a admin*");
            Correo.To.Add(correo);
            Correo.Subject = ("Nuevo Mensaje desde Contactanos");
            Correo.Body = "Hola! Tienes un nuevo mensaje desde el formulario de contacto: " + Environment.NewLine
                          + "Nombre y Apellido: " + modelo.nombreApellido + Environment.NewLine + "Email: " + modelo.email
                          + Environment.NewLine + "Teléfono: " + modelo.telefono + Environment.NewLine +
                          "Mensaje: " + modelo.mensaje;
            Correo.Priority = MailPriority.Normal;

            SmtpClient ServerEmail = new SmtpClient();
            ServerEmail.Credentials = new NetworkCredential("*Solicitar a admin*", "*Solicitar a admin*");
            ServerEmail.Host = "smtp.gmail.com";
            ServerEmail.Port = 587;
            ServerEmail.EnableSsl = true;
            try
            {
                ServerEmail.Send(Correo);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            Correo.Dispose();
        }

        public ActionResult TerminosYCondiciones()
        {
            return View();
        }

       
        public ActionResult Contactenos()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Contactenos(MensajeContacto modelo)
        {

            if (ModelState.IsValid)
            {
                mailContacto(modelo);
                return RedirectToAction("mensajeEnviado", "Home");
            }
            return View();
        }

        public ActionResult mensajeEnviado()
        {
            return View();
        }



        public ActionResult CovidRecomendaciones()
        {
            return View();
        }

        public ActionResult ConsejosRedesSociales()
        {
            return View();
        }

        public ActionResult ConsejosWhatsapp()
        {
            return View();
        }

        public ActionResult ConsejosCuentasBancarias()
        {
            return View();
        }

    }
}