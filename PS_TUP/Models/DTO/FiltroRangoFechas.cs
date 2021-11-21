using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PS_TUP.Models.DTO
{
    public class FiltroRangoFechas
    {

        public int cantidad { get; set; }
        public int IdTipoDenuncia { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaDesde { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaHasta { get; set; }
    }
}