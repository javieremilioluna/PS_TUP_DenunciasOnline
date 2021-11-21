using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PS_TUP.Models.GestionUsuarios
{
    public class DenunciasSQL
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
        public byte[] Audio1 { get; set; }
        public byte[] Audio2 { get; set; }

    }
}