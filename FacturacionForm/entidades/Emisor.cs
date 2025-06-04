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
        public string NIT { get; set; } = "04071508151011";
        public string NRC { get; set; } = "2432212";
        public string Telefono { get; set; } = "23352785";
        public string Direccion { get; set; }="Final 3 avenida sur, final barrio el chile, calle a la sierpe, Chalatenango";
        public string Email { get; set; } = "recepcion-montepiedad@enviosdte.email";
        public string CodigoDepartamento { get; set; } = "04";
        public string CodigoDistrito { get; set; } = "35";
        public string CodigoActividadEconomica { get; set; } = "47739";
        public string ActividadEconomica { get; set; } = "Venta al por menor de otros productos ncp";
        public string usuariofirmador = "04071508151011";
        public string Clave { get; set; } = "ParqueSanto86";
        public string ClaveApi { get; set; } = "Ana,Herrera10";

    }
}
