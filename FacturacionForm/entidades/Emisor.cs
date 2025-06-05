using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionForm.entidades
{
    public class Emisor
    {
        public string NombreComercial { get; set; }
        public string NIT { get; set; } = "";
        public string NRC { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Direccion { get; set; }="Final 3 avenida sur, final barrio el chile, calle a la sierpe, Chalatenango";
        public string Email { get; set; } = "";
        public string CodigoDepartamento { get; set; } = "04";
        public string CodigoDistrito { get; set; } = "35";
        public string CodigoActividadEconomica { get; set; } = "47739";
        public string ActividadEconomica { get; set; } = "Venta al por menor de otros productos ncp";
        public string usuariofirmador = "";
        public string Clave { get; set; } = "";
        public string ClaveApi { get; set; } = "";

    }
}
