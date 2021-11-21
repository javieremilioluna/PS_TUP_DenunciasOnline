using PS_TUP.AccesoDatos;
using PS_TUP.Models;
using PS_TUP.Models.DTO;
using PS_TUP.Models.TipoEmpresa;
using PS_TUP.Models.TiposUsuario;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;
using PS_TUP.Models.Acceso;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PS_TUP.Controllers
{

    public class CuentaController : Controller
    {
        private PS_TUPEntities db = new PS_TUPEntities();

        // GET: Cuenta
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            using (var context = new PS_TUPEntities())
            {
                bool isValid = context.Usuarios.Any(x => x.UserName == usuario.UserName && x.Password == usuario.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(usuario.UserName, false);
                    return RedirectToAction("VerDatos", "Cuenta");
                }

                else
                {
                    ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                    return View();
                }
            }

        }

        // GET
        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult VerificarEmpresa()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registro(Usuario model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new PS_TUPEntities())
            {
                bool exist = db.Usuarios.Count(x => x.UserName == model.UserName) == 1;
                bool mail = db.Usuarios.Count(x => x.Email == model.Email) == 1;
                if (exist)
                {
                    ModelState.AddModelError("", "El usuario ingresado ya existe, ingrese uno distinto.");
                    return View();
                }

                else if (mail)
                {
                    ModelState.AddModelError("", "El email ingresado ya existe, ingrese uno distinto.");
                    return View();
                }

                else
                {
                    Usuarios user = new Usuarios();
                    user.UserName = model.UserName;
                    user.Password = model.Password;
                    user.Email = model.Email;
                    user.Role = model.role;

                    db.Usuarios.Add(user);
                    db.SaveChanges();

                    
                }
                return View("RegistroExitoso");
            }



            //return RedirectToAction("RegistroExitoso");
        }

        public ActionResult RegistroExitoso()
        {
            return View();
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Usuario, Empresa, Fiscalia, Administrador")]
        [Route("Escritorio")]
        public ActionResult VerDatos()
        {

            return View("VerDatos");      
         }

        [Authorize]
        //GET
        public ActionResult CambiarContraseña(string userName)
        {
            var mail = (from u in db.Usuarios
                        where u.UserName == User.Identity.Name
                        select u.Email).FirstOrDefault();
            string email = mail.ToString();
            ViewBag.mail = email;

            return View("CambiarContraseña");
        }
        //GET
        public ActionResult ContraseñaCambiada()
        {

            return View("ContraseñaCambiada");
        }

        [Authorize]
        [HttpPost]
        public ActionResult CambiarContraseña(Contraseña modelo)
        {
            if (ModelState.IsValid)
            {
                bool resultado = GestorBD.ActualizarContraseña(modelo);
                if (resultado)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("ContraseñaCambiada");
                }
                else
                {
                    return View(modelo);
                }
            }
            return View();
        }



        [Authorize]
        //GET
        public ActionResult ActualizarMail(string userName)
        {
            var mail = (from u in db.Usuarios
                        where u.UserName == User.Identity.Name
                        select u.Email).FirstOrDefault();
            string email = mail.ToString();
            ViewBag.mail = email;

            return View("ActualizarMail");
        }
        //GET

        public ActionResult MailCambiado()
        {

            return View("MailCambiado");
        }

        [Authorize]
        [HttpPost]
        public ActionResult ActualizarMail(Email modelo)
        {

            var mail2 = (from u in db.Usuarios
                        where u.UserName == User.Identity.Name
                        select u.Email).FirstOrDefault();
            string email = mail2.ToString();
            ViewBag.mail2 = email;

            if (ModelState.IsValid)
            {
                bool resultado = GestorBD.ActualizarEmail(modelo);
                if (resultado)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("MailCambiado");
                }
                else
                {
                    return View(modelo);
                }
            }
            return View();
        }


        [Authorize]
        //GET
        public ActionResult ActualizarDatosTipoUsuario(string UserName)
        {
            List<Provincias> listaProvincias = GestorBD.ObtenerProvincias();
            List<SelectListItem> itemsCombo = listaProvincias.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.nombreProvincia,
                    Value = p.idProvincia.ToString(),
                    Selected = false //para asegurarnos que no haya ninguno seleccionado
                };
            });


            RegistroTipoUsuario resultado = GestorBD.ObtenerUsuario(UserName);

            foreach (var item in itemsCombo)
            {
                if (item.Value.Equals(resultado.idProvincia.ToString()))
                // if (item.Value.Equals(resultado.idProvincia.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }

            ViewBag.items = itemsCombo;
            ViewBag.nombre = resultado.nombre + " " + resultado.apellido;
            return View("ActualizarDatosTipoUsuario", resultado);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ActualizarDatosTipoUsuario(RegistroTipoUsuario modelo)
        {

            List<Provincias> listaProvincias = GestorBD.ObtenerProvincias();
            List<SelectListItem> itemsCombo = listaProvincias.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.nombreProvincia,
                    Value = p.idProvincia.ToString(),
                    Selected = false //para asegurarnos que no haya ninguno seleccionado
                };
            });

            foreach (var item in itemsCombo)
            {
                if (item.Value.Equals(modelo.idProvincia.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }

            ViewBag.items = itemsCombo;

       
            if (ModelState.IsValid)
            {
               
                bool resultado = GestorBD.ActualizarDatosUsuario(modelo);
                if (resultado)
                {
                    return RedirectToAction("VerDatos", "Cuenta");
                }
                else
                {
                    return View(modelo);
                }
            }
            return View();


        }


        ///////////////////////////////////////////////////
        
        [Authorize]
        //GET
        public ActionResult ActualizarDatosTipoEmpresa(string UserName)
        {
            List<Provincias> listaProvincias = GestorBD.ObtenerProvincias();
            List<SelectListItem> itemsCombo = listaProvincias.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.nombreProvincia,
                    Value = p.idProvincia.ToString(),
                    Selected = false //para asegurarnos que no haya ninguno seleccionado
                };
            });


            RegistroTipoEmpresa resultado = GestorBD.ObtenerEmpresa(UserName);

            foreach (var item in itemsCombo)
            {
                if (item.Value.Equals(resultado.idProvincia.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }

            ViewBag.items = itemsCombo;     
            return View("ActualizarDatosTipoEmpresa", resultado);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ActualizarDatosTipoEmpresa(RegistroTipoEmpresa modelo)
        {
            

            List<Provincias> listaProvincias = GestorBD.ObtenerProvincias();
            List<SelectListItem> itemsCombo = listaProvincias.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.nombreProvincia,
                    Value = p.idProvincia.ToString(),
                    Selected = false
                };
            });

            foreach (var item in itemsCombo)
            {
                if (item.Value.Equals(modelo.idProvincia.ToString()))
                {
                    item.Selected = true;
                    break;
                }
            }
            ViewBag.items = itemsCombo;


            if (ModelState.IsValid)

            {
                
                bool resultado = GestorBD.ActualizarDatosEmpresa(modelo);
                if (resultado)
                {
                    
                    return RedirectToAction("VerDatos", "Cuenta");
                }
                else
                {
                    return View(modelo);
                }
            }
            return View();
        }

        ////////////////////////////// 
        [Authorize]
        //GET
        public ActionResult CambiarLogo(string UserName)
        {
            
            RegistroTipoEmpresa resultado = GestorBD.ObtenerLogoEmpresa(UserName);         
            return View("CambiarLogo", resultado);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CambiarLogo(RegistroTipoEmpresa modelo)
        {         
            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase.ContentLength == 0)
            {
                RegistroTipoEmpresa resultado1 = GestorBD.ObtenerLogoEmpresa(modelo.UserName);
                byte[] byteImage2 = resultado1.logo;
                modelo.logo = byteImage2;
            }

            else 
            {

                WebImage image = new WebImage(FileBase.InputStream);
                modelo.logo = image.GetBytes();                             
            }

            GestorBD.ActualizarLogoEmpresa(modelo);
            return RedirectToAction("VerDatos", "Cuenta");

        }


        [Authorize]
        //GET
        public ActionResult CambiarComprobante(string UserName)
        {

            RegistroTipoEmpresa resultado = GestorBD.ObtenerComprobanteEmpresa(UserName);
            return View("CambiarComprobante", resultado);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CambiarComprobante(RegistroTipoEmpresa modelo)
        {
            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase.ContentLength == 0)
            {
                RegistroTipoEmpresa resultado = GestorBD.ObtenerComprobanteEmpresa(modelo.UserName);
                byte[] byteImage = resultado.comprobanteInsc;
                modelo.comprobanteInsc = byteImage;
            }

            else
            {

                WebImage image = new WebImage(FileBase.InputStream);
                modelo.comprobanteInsc = image.GetBytes();
            }

            GestorBD.ActualizarComprobanteEmpresa(modelo);
            return RedirectToAction("VerDatos", "Cuenta");

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



        public ActionResult getComprobante(string UserName)
        {
            RegistroTipoEmpresa resultado = GestorBD.ObtenerEmpresa(UserName);
            byte[] byteImage = resultado.comprobanteInsc;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");

        }

    
        [AllowAnonymous]
        public void enviarEMail(string correo, string password)
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
                Correo.Subject = ("Recuperar Contraseña en DenunciasOnline");
                Correo.Body = "Hola! Usted solicito recuperar su contraseña en el sitio web Denuncias Online, su clave es: " + password;
                Correo.Priority = MailPriority.Normal;
                ServerEmail.Send(Correo);
                Correo.Dispose();
            }

        }

        public ActionResult RecuperarContraseña()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarContraseña([Bind(Include = "Id,UserName,Password,Role,Email")] Usuario model)
        {

       

            bool mail = db.Usuarios.Count(x => x.Email == model.Email) == 0;

            var us = from u in db.Usuarios
                     where u.Email == model.Email
                     select u;

            Usuarios usuario = us.FirstOrDefault();


            if (mail)
            {
                ModelState.AddModelError("", "El email ingresado no pertenece a ningún usuario o el campo está vacío.");
                return View();
            }

            //else if (usuario == null)
            //{
            //    ModelState.AddModelError("", "Debe ingresar un Email");
            //    return View();
            //}

            else
            {
                 enviarEMail(usuario.Email, usuario.Password);
                 return RedirectToAction("ContOlvidadaMailEnviado", "Cuenta");
            }
           

        }

        public ActionResult ContOlvidadaMailEnviado()
        {
            return View();
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