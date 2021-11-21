//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PS_TUP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class MailsEmpresas
    {
        public int IdMailEmpresa { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        //[EmailAddress]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "El email debe incluir '@' y un dominio '.com' por ejemplo.")]
        public string Mail { get; set; }
    }
}
