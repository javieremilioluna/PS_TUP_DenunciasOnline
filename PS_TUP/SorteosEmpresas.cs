//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

//namespace PS_TUP
//{
//    using System;
//    using System.Collections.Generic;
    
//    public partial class SorteosEmpresas
//    {
//        public int IdSorteo { get; set; }
//        public string UserName { get; set; }
//        public string Titulo { get; set; }
//        public string Descripcion { get; set; }
//        public Nullable<System.DateTime> FechaInicio { get; set; }
//        public Nullable<System.DateTime> FechaFin { get; set; }
//        public string Ganadores { get; set; }
//        public string Activo { get; set; }
//    }
//}



namespace PS_TUP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SorteosEmpresas
    {
        public int IdSorteo { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }
        //public Nullable<System.DateTime> FechaFin { get; set; }

        public string Ganadores { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Activo { get; set; }
    }

    public enum Activo
    {
        Si,
        No
    }
}