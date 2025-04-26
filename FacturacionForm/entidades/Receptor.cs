namespace FacturacionForm.entidades
{
    public class Receptor
    {
        public string NombreComercial { get; set; }
        public string NIT { get; set; }
        public string NRC { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string CodigoDepartamento { get; set; } = "34";
        public string CodigoDistrito { get; set; } = "05";
        public string CodigoActividadEconomica { get; set; } = "4615";
        public string ActividadEconomica { get; set; } = "Venta de vehiculos automotores";


    }
}
