using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PS_TUP.Models
{
    public class Contraseña
    {
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(15, ErrorMessage = "Limite: 15 caracteres.")]
        public string Password { get; set; }
        public string UserName { get; set; }

    }
}