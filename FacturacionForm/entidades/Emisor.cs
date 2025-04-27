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
        public string NIT { get; set; }
        public string NRC { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string CodigoDepartamento { get; set; } = "04";
        public string CodigoDistrito { get; set; } = "35";
        public string CodigoActividadEconomica { get; set; } = "46510";
        public string ActividadEconomica { get; set; } = "VENTA AL POR MAYOR DE COMPUTADORAS, EQUIPO PERIFERICO Y PROGRAMAS INFORMATICOS";

        public string Clave { get; set; } = "Keyjo@Pri9";
        public string ClaveApi { get; set; } = "Keyjo@2025";

    }
}
