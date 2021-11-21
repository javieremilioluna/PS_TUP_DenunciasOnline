using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PS_TUP.Models.TipoEmpresa
{
    public class RegistroTipoEmpresa
    {

        public int idEmpresa { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(25, ErrorMessage = "Limite: 25 caracteres.")]
        public string  CUIT { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(45, ErrorMessage = "Limite: 45 caracteres.")]
        public string razonSocial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(45, ErrorMessage = "Limite: 45 caracteres.")]
        public string nombreComercial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int idProvincia { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(45, ErrorMessage = "Limite: 45 caracteres.")]
        public string ciudad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(45, ErrorMessage = "Limite: 45 caracteres.")]
        public string calle { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(15, ErrorMessage = "Limite: 15 caracteres.")]
        public string altura { get; set; }

       // [Required(ErrorMessage = "Este campo es obligatorio. Si no tiene piso ingrese PB.")]
        [MaxLength(10, ErrorMessage = "Limite: 10 caracteres.")]
        public string piso { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(90, ErrorMessage = "Limite: 90 caracteres.")]
        public string paginaWeb { get; set; }

        public byte[] logo { get; set; }       
        public byte[] comprobanteInsc { get; set; }



    }


    //public enum TipoRedSocial
    //{
    //    Instagram,
    //    Twitter,
    //    Facebook,
    //    Otra
    //}
}