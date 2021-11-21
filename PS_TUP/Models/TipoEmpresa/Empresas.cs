using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PS_TUP.Models.TipoEmpresa
{
    public class Empresas
    {
   

        public int idEmpresa { get; set; }
        public string UserName { get; set; }
        public string CUIT { get; set; }
        public string razonSocial { get; set; }
        public string nombreComercial { get; set; }
        public string provincia { get; set; }
        public string ciudad { get; set; }
        public string calle { get; set; }
        public string altura { get; set; }
        public string piso { get; set; }
        public string paginaWeb { get; set; }

        public byte[] logo { get; set; }
        public byte[] comprobanteInsc { get; set; }
        public string verificada { get; set; }

    }
}