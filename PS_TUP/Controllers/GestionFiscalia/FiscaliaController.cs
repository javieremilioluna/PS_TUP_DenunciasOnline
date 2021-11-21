using PS_TUP.AccesoDatos;
using PS_TUP.Models.GestionFiscalia;
using PS_TUP.Models.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using PS_TUP.Models.DTO;

namespace PS_TUP.Controllers.GestionFiscalia
{
    [Authorize(Roles = "Fiscalia")]
    public class FiscaliaController : Controller
    {
        private PS_TUPEntitiesGestionUsuarios db = new PS_TUPEntitiesGestionUsuarios();
       
        // GET: Fiscalia
        public ActionResult DenunciasAbiertas(string search, int? i)
        {   
            List<DenunciaDesdeFiscalia> denunciasAbiertas = GestorBDFiscalia.ObtenerDenunciasAbiertas();
            return View(denunciasAbiertas.ToPagedList(i ?? 1, 3));   //el 3 representa la cantidad de filas devueltas  
        }


        public ActionResult ResultadoBusquedaDenunciasAbiertas(string DNI)
        {
            List<DenunciaDesdeFiscalia> denunciasAbiertas = GestorBDFiscalia.ObtenerDenunciasAbiertasPorDNI(DNI);
            return View(denunciasAbiertas);
        }

        public ActionResult DenunciasCerradas(string search, int? i)
        {
            List<DenunciaDesdeFiscalia> denunciasCerradas = GestorBDFiscalia.ObtenerDenunciasCerradas();
            return View(denunciasCerradas.ToPagedList(i ?? 1, 3));
        }


        public ActionResult ResultadoBusquedaDenunciasCerradas(string DNI)
        {
            List<DenunciaDesdeFiscalia> denunciasCerradas = GestorBDFiscalia.ObtenerDenunciasCerradasPorDNI(DNI);
            return View(denunciasCerradas);
        }


        // GET: Denuncias/Details/5
        public ActionResult DetalleDenunciaVistaFiscalia(int id)
        {
            DenunciaDesdeFiscalia resultado = GestorBDFiscalia.ObtenerDenunciaPorId(id);
            return View("DetalleDenunciaVistaFiscalia", resultado);
        }




        public ActionResult DetalleDenunciaCerradaVistaFiscalia(int id)
        {
            DenunciaDesdeFiscalia resultado = GestorBDFiscalia.ObtenerDenunciaPorId(id);
            return View("DetalleDenunciaCerradaVistaFiscalia", resultado);
        }

        //LO HAGO SOLO CON GET
        public ActionResult CerrarDenuncia(int id)
        {
            GestorBDFiscalia.CerrarDenuncia(id);
            return RedirectToAction("DenunciasCerradas");
        }

        public ActionResult ReabrirDenuncia(int id)
        {
            GestorBDFiscalia.ReabrirDenuncia(id);
            return RedirectToAction("DenunciasAbiertas");
        }

        public ActionResult Reportes()
        {
            List<DenunciasPorMes> listado = GestorBDReportes.DenunciasPorMes();
            List<CantDenunciasPorTipo> listado2 = GestorBDReportes.CantDenunciasPorTipos();

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


            ViewBag.DenunciasPorMes = listado;
            ViewBag.resultado = listado2;
            return View();
        }

        public ActionResult ResultadosBusqueda(DateTime fechaDesde, DateTime fechaHasta)
        {

            if (ModelState.IsValid)
            {
                List<FiltroRangoFechas> listado = GestorBDReportes.FiltroCantDenunciasPorTipos(fechaDesde, fechaHasta);
                ViewBag.fechaDesde = fechaDesde;
                ViewBag.fechaHasta = fechaHasta;
                ViewBag.resultado = listado;

                return View();

                // return View(listado);
            }

            return View();


            //List<FiltroRangoFechas> listado = GestorBDReportes.FiltroCantDenunciasPorTipos(fechaDesde, fechaHasta);
            //ViewBag.fechaDesde = fechaDesde;
            //ViewBag.fechaHasta = fechaHasta;
            //return View(listado);
        }


        public ActionResult ResultadosBusquedaEstadistica(string IdTipoDenuncia)
        {

            if (ModelState.IsValid)
            {
                List<DenunciasPorAnio> listado = GestorBDReportes.DenunciasPorAnioPorTipo(IdTipoDenuncia);
                ViewBag.resultado = listado;

                return View(listado);
            }

            return View();

        }

    }
}