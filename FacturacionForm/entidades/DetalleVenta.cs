namespace FacturacionForm.entidades
{
    public class DetalleVenta
    {
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Total => Cantidad * Precio; // Se calcula automáticamente

        public DetalleVenta(int cantidad, string descripcion, decimal precio)
        {
            Cantidad = cantidad;
            Descripcion = descripcion;
            Precio = precio;
        }
    }
}
