using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PS_TUP.Models.DTO
{
    public class Busqueda
    {
        [Required(ErrorMessage = "Campo requerido")]
        public string nombreComercial { get; set; }
    }
}