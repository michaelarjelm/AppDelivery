using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Shared.Modelos
{
    public class Comuna
    {
        public string? Key { get; set; }
        public string? name { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public Regiones? Region { get; set; }
    }
}
