using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Shared.Modelos
{
    public class Direccion
    {
        public string? Key { get; set; }
        public string? Calle { get; set; }
        public string? Numero { get; set; }
        public Comuna? Comuna { get; set; }
        public string? CodigoPostal { get; set; }
        public bool? EsPrincipal { get; set; }
        public string DireccionCompleta => $"{Calle} {Numero}";
    }
}
