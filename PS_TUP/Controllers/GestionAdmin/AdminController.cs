using PagedList;
using PS_TUP.AccesoDatos;
using PS_TUP.Models.DTO;
using PS_TUP.Models.Reportes;
using PS_TUP.Models.TipoEmpresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PS_TUP.Controllers.GestionAdmin
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
      
        private PS_TUPEntitiesGestionEmpresas db = new PS_TUPEntitiesGestionEmpresas();
        
    
        public ActionResult EmpresasVerificadas(string search, int? i)
        {
            List<Empresas> listado = GestorBDBuscadorListadoEmpresas.ObtenerEmpresasListado();
            int id = 1033;
            Empresas comprobanteModelo = GestorBDBuscadorListadoEmpresas.ObtenerComprobanteModelo(id);
            decimal modelo = Convert.ToDecimal(BitConverter.ToInt64(comprobanteModelo.comprobanteInsc, 0));

            ViewBag.comparador = modelo;
            return View(listado.ToPagedList(i ?? 1, 4));  
        }


        public ActionResult ResultadoBusquedaEmpresasVerificadas( string search, int? i, string nombreComercial)
        {
            
                List<Empresas> listado = GestorBDBuscadorListadoEmpresas.BuscarEmpresasVerificadas(nombreComercial);
                int id = 1033;
                Empresas comprobanteModelo = GestorBDBuscadorListadoEmpresas.ObtenerComprobanteModelo(id);
                decimal modelo = Convert.ToDecimal(BitConverter.ToInt64(comprobanteModelo.comprobanteInsc, 0));
                ViewBag.comparador = modelo;
                return View(listado.ToPagedList(i ?? 1, 4));
        }

        public ActionResult EmpresasNoVerificadas(string search, int? i)
        {
            List<Empresas> listado = GestorBDBuscadorListadoEmpresas.ObtenerEmpresasNoVerificadas();
            int id = 1033;
            Empresas comprobanteModelo = GestorBDBuscadorListadoEmpresas.ObtenerComprobanteModelo(id);
            decimal modelo = Convert.ToDecimal(BitConverter.ToInt64(comprobanteModelo.comprobanteInsc, 0));
            ViewBag.comparador = modelo;
            return View(listado.ToPagedList(i ?? 1, 4));
        }

        public ActionResult ResultadoBusquedaEmpresasNoVerificadas(string nombreComercial, string search, int? i)
        {
            List<Empresas> listado = GestorBDBuscadorListadoEmpresas.BuscarEmpresasNoVerificadas(nombreComercial);
            int id = 1033;
            Empresas comprobanteModelo = GestorBDBuscadorListadoEmpresas.ObtenerComprobanteModelo(id);
            decimal modelo = Convert.ToDecimal(BitConverter.ToInt64(comprobanteModelo.comprobanteInsc, 0));
            ViewBag.comparador = modelo;
            return View(listado.ToPagedList(i ?? 1, 4));
        }

        public FileContentResult Download(int id)
        {
            byte[] Data;
            Empresas empresa = GestorBDBuscadorListadoEmpresas.ObtenerEmpresa(id);
            Data = (byte[])empresa.comprobanteInsc.ToArray();
            return File(Data, "Text");
        }


        public ActionResult QuitarVerificacion(int id)
        {
            GestorBDBuscadorListadoEmpresas.QuitarVerificacion(id);
            return RedirectToAction("EmpresasNoVerificadas");
        }

        public ActionResult VerificarEmpresa(int id)
        {
            GestorBDBuscadorListadoEmpresas.VerificarEmpresa(id);
            return RedirectToAction("EmpresasVerificadas");
        }



        public ActionResult DetalleEmpresaNoVerificada(int idEmpresa)
        {
            Empresas empresa = GestorBDBuscadorListadoEmpresas.ObtenerEmpresa(idEmpresa);
            return View(empresa);
        }


        public ActionResult Reportes()
        {
            CantidadEmprVerificadas cantidadVerificadas = GestorBDReportes.EmprVerificadas();
            CantidadEmprVerificadas noVerificadas = GestorBDReportes.EmprNoVerificadas();
            CantRegPorTipo cantEmpresas = GestorBDReportes.CantidadEmpresasRegistradas();
            CantRegPorTipo cantUsuarios = GestorBDReportes.CantidadUsuariosRegistradas();

            double noV = noVerificadas.porcentaje;
            ViewBag.noVerif = noV;
            //ViewBag.noVerif = noVerificadas.porcentaje;
            ViewBag.cantEmpresas = cantEmpresas.cantidad;
            ViewBag.cantUsuarios = cantUsuarios.cantidad;

            return View(cantidadVerificadas);
        }

       
    }
}