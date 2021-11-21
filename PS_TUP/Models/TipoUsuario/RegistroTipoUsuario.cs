using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PS_TUP.Models.TiposUsuario
{
    public class RegistroTipoUsuario
    {

        public int id_usuario { get; set; }
        public string UserName { get; set; }

        
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Limite: 10 caracteres.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(45, ErrorMessage = "Limite: 45 caracteres.")]
        public string nombre { get; set; }
      
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(45, ErrorMessage = "Limite: 45 caracteres.")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]  
        public DateTime fecha_nac { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string sexo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Limite: 20 caracteres.")]
        public string telefono { get; set; }

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

        //[Required(ErrorMessage = "Este campo es obligatorio. Si no tiene piso ingrese PB.")]
        [MaxLength(10, ErrorMessage = "Limite: 10 caracteres.")]
        public string piso { get; set; }

        public int datosActualizados { get; set; }

    }

    public enum Sexo
    {
        Mujer,
        Hombre,
        Otro
    }

}