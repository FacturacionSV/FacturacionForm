using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionForm.entidades
{
    public class Emisor
    {
        public string NombreComercial { get; set; } = "GRUPO , S.A. de C.V.";
        public string NIT { get; set; } = "06140811221056";
        public string NRC { get; set; } = "3214161";
        public string Telefono { get; set; } = "23564512";
        public string Direccion { get; set; } = "45 av sur local 36 Centro Profesional";
        public string Email { get; set; } = "comprobantes@enviosdte.email";
        public string CodigoDepartamento { get; set; } = "06";
        public string CodigoDistrito { get; set; } = "06";
        public string CodigoActividadEconomica { get; set; } = "46599";
        public string ActividadEconomica { get; set; } = "Venta al por menor de otros productos ncp";
        public string usuariofirmador = "06140811221056";
        public string Clave { get; set; } = "INDECO123";
        public string ClaveApi { get; set; } = "Th2027kkll#";

    }
}
