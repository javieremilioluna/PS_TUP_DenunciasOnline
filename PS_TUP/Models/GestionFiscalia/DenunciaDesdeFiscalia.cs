using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PS_TUP.Models.GestionFiscalia
{
    public class DenunciaDesdeFiscalia
    {
        public int IdDenuncia { get; set; }
        public string IdTipoDenuncia { get; set; }
        public string Descripcion { get; set; }
        public byte[] Foto1 { get; set; }
        public byte[] Foto2 { get; set; }
        public byte[] Archivo { get; set; }
        public string Fiscalia { get; set; }
        public string UserName { get; set; }
        public DateTime Fecha { get; set; }
        public string activa { get; set; }
        public int id_usuario { get; set; }
        public string DNI { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nac { get; set; }
        public string sexo { get; set; }
        public string telefono { get; set; }
        public string nombreProvincia { get; set; }
        public string ciudad { get; set; }
        public string calle { get; set; }
        public string altura { get; set; }
        public string piso { get; set; }
        public string Email { get; set; }
        public byte[] Audio1 { get; set; }
        public byte[] Audio2 { get; set; }
    }
}