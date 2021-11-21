using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PS_TUP.Models.Acceso
{
    public class Email
    {

        [Required(ErrorMessage = "Debe ingresar un e-mail válido.")]
        [EmailAddress]
        public string Mail { get; set; }
        public string UserName { get; set; }
    }
}