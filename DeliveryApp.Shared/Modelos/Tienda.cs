namespace DeliveryApp.Shared.Modelos
{
    public class Tienda
    {
        public string? Key { get; set; }
        public string? Cabecera { get; set; }
        public string? Logo { get; set; }
        public string? Nombre { get; set; }
        public Direccion? Direccion { get; set; }
        public string? TiempoEntrega { get; set; }
        public int? Minimo { get; set; }
        public int? CuotaServicio { get; set; }
        public double? Calificacion { get; set; }
    }
}
