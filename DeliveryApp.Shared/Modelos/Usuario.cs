using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Shared.Modelos
{
    public class Usuario
    {
        public string? Key { get; set; }
        public string? PrimerNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? CorreoElectronico { get; set; }
        public Direccion? Direccion { get; set; }
        public bool? Estado { get; set; }

        public string? NombreCompleto => $"{PrimerNombre} {PrimerApellido}";
        public string? EstadoTexto => Estado.HasValue ? (Estado.Value ? "Activo" : "Inactivo") : "Estado Desconocido";
    }
}
