using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace PS_TUP.Models
{
    public class Usuario
    {
     
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Debe ingresar un usuario válido.")]
        [MaxLength(20, ErrorMessage = "Limite: 20 caracteres.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña no puede estar vacía.")]
        [DataType(DataType.Password, ErrorMessage = "Contraseña no válida.")]
        [MaxLength(15, ErrorMessage = "Limite: 15 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe ingresar un tipo de registro válido.")]
        public string role { get; set; }

        [Required(ErrorMessage = "Debe ingresar un e-mail válido.")]
        [EmailAddress]
        //[RegularExpression(@"^([A-Za-z0-9][^'!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ][a-zA-z0-9-._][^!&\\#*$%^?<>()+=:;`~\[\]{}|/,₹€@ ]*\@[a-zA-Z0-9][^!&@\\#*$%^?<>()+=':;~`.\[\]{}|/,₹€ ]*\.[a-zA-Z]{2,6})$", ErrorMessage = "Please enter a valid Email")]
        public string Email { get; set; }

    }

    public enum TipoUsuario
    {
        Usuario,
        Empresa
    }
}