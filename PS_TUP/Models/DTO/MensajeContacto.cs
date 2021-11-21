using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PS_TUP.Models.DTO
{
    public class MensajeContacto
    {
        [Required(ErrorMessage = "Debe ingresar un nombre y apellido.")]
        public string nombreApellido { get; set; }
        [Required(ErrorMessage = "Debe ingresar un e-mail válido.")]
        [EmailAddress]
        public string email { get; set; }
        [Required(ErrorMessage = "Debe ingresar un teléfono válido.")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Debe ingresar un mensaje válido.")]
        public string mensaje { get; set; }


    }
}