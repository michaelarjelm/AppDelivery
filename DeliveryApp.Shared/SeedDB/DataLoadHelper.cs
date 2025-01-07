using DeliveryApp.Shared.Helpers;
using DeliveryApp.Shared.Modelos;
using System.Text.Json;

namespace DeliveryApp.Shared.SeedDB
{
    public static class DataLoadHelper
    {
        private const string ApiBaseUrl = "https://api.countrystatecity.in/v1";
        private const string ApiKey = "NUZicm9hR0FUb0oxUU5mck14NEY3cEFkcU9GR3VqdEhVOGZlODlIRQ==";

        public static async Task ActualizarPaisesRegionesYComunas()
        {
            try
            {
                var paisesExistentes = await DatabaseHelper.GetAllDataAsync<Pais>("Paises");

                if (paisesExistentes?.Any(p => p.iso2 == "CL") == true)
                {
                    Console.WriteLine("Chile ya está registrado en la base de datos.");
                    return;
                }

                var pais = await ObtenerPais("CL");

                var paisKey = await DatabaseHelper.CreateDataAsync("Paises", pais);

                var regiones = await ObtenerRegiones(pais.iso2);

                foreach (var region in regiones)
                {
                    region.Pais = null;

                    var regionKey = await DatabaseHelper.CreateDataAsync($"Paises/{paisKey}/Regiones", region);

                    // Obtener comunas de la región
                    var comunas = await ObtenerComunas(pais.iso2, region.iso2);

                    foreach (var comuna in comunas)
                    {
                        comuna.Region = null;

                        await DatabaseHelper.CreateDataAsync($"Paises/{paisKey}/Regiones/{regionKey}/Comunas", comuna);
                        Console.WriteLine($"Comuna '{comuna.name}' registrada bajo la región '{region.name}'.");
                    }
                }

                Console.WriteLine("Paises, regiones y comunas fueron registradas correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar datos: {ex.Message}");
            }
        }
        private static async Task<Pais> ObtenerPais(string iso2)
        {
            using var client = CrearHttpClient();
            var response = await client.GetAsync($"{ApiBaseUrl}/countries/{iso2}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Pais>(json);
        }
        private static async Task<List<Regiones>> ObtenerRegiones(string countryId)
        {
            using var client = CrearHttpClient();
            var response = await client.GetAsync($"{ApiBaseUrl}/countries/{countryId}/states");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Regiones>>(json);
        }
        private static async Task<List<Comuna>> ObtenerComunas(string countryId, string stateId)
        {
            using var client = CrearHttpClient();
            var response = await client.GetAsync($"{ApiBaseUrl}/countries/{countryId}/states/{stateId}/cities");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Comuna>>(json);
        }
        private static HttpClient CrearHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-CSCAPI-KEY", ApiKey);
            return client;
        }

        public static async Task RegistrarTiendas()
        {
            var tiendasExistentes = await DatabaseHelper.GetAllDataAsync<Tienda>("Tiendas");

            if (tiendasExistentes.Count == 0)
            {
                var tiendas = new List<Tienda>
                {
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's República",
                Direccion = new Direccion
                {
                    Calle = "Av. República",
                    Numero = "40",
                    Comuna = new Comuna
                    {
                        name = "Santiago Centro",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 40 minutos",
                Minimo = 7000,
                CuotaServicio = 500,
                Calificacion = 4.5
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Mall Estación Central",
                Direccion = new Direccion
                {
                    Calle = "Exposición",
                    Numero = "1",
                    Comuna = new Comuna
                    {
                        name = "Estación Central",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 6500,
                CuotaServicio = 400,
                Calificacion = 4.3
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Mall Plaza Alameda",
                Direccion = new Direccion
                {
                    Calle = "Avda. Libertador Bernardo O'Higgins",
                    Numero = "3470",
                    Comuna = new Comuna
                    {
                        name = "Estación Central",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "20 - 30 minutos",
                Minimo = 6000,
                CuotaServicio = 300,
                Calificacion = 4.2
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Ahumada",
                Direccion = new Direccion
                {
                    Calle = "Ahumada",
                    Numero = "12",
                    Comuna = new Comuna
                    {
                        name = "Santiago Centro",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 40 minutos",
                Minimo = 8000,
                CuotaServicio = 550,
                Calificacion = 4.6
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Mall Patio Centro",
                Direccion = new Direccion
                {
                    Calle = "Bandera",
                    Numero = "101",
                    Comuna = new Comuna
                    {
                        name = "Santiago Centro",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "35 - 45 minutos",
                Minimo = 7500,
                CuotaServicio = 450,
                Calificacion = 4.4
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Santa Rosa/Alameda",
                Direccion = new Direccion
                {
                    Calle = "Av. Libertador Bernardo O'Higgins",
                    Numero = "666 y 668",
                    Comuna = new Comuna
                    {
                        name = "Santiago Centro",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 50 minutos",
                Minimo = 8500,
                CuotaServicio = 400,
                Calificacion = 4.1
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Estación de Buses",
                Direccion = new Direccion
                {
                    Calle = "Nicasio Retamales",
                    Numero = "44",
                    Comuna = new Comuna
                    {
                        name = "Estación Central",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 6000,
                CuotaServicio = 350,
                Calificacion = 4.2
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Lider General Velásquez",
                Direccion = new Direccion
                {
                    Calle = "Av. Padre Hurtado",
                    Numero = "#060",
                    Comuna = new Comuna
                    {
                        name = "Estación Central",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 6500,
                CuotaServicio = 350,
                Calificacion = 4.4
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Mall del Centro",
                Direccion = new Direccion
                {
                    Calle = "Puente",
                    Numero = "689",
                    Comuna = new Comuna
                    {
                        name = "Santiago Centro",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 40 minutos",
                Minimo = 7000,
                CuotaServicio = 400,
                Calificacion = 4.3
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Bilbao Bustamante",
                Direccion = new Direccion
                {
                    Calle = "Av. Bilbao",
                    Numero = "0103",
                    Comuna = new Comuna
                    {
                        name = "Providencia",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "20 - 30 minutos",
                Minimo = 6000,
                CuotaServicio = 300,
                Calificacion = 4.2
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Bellavista",
                Direccion = new Direccion
                {
                    Calle = "Av. Bellavista",
                    Numero = "052",
                    Comuna = new Comuna
                    {
                        name = "Providencia",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "20 - 30 minutos",
                Minimo = 5500,
                CuotaServicio = 250,
                Calificacion = 4.1
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Mall Barrio Independencia",
                Direccion = new Direccion
                {
                    Calle = "Avda. Independencia",
                    Numero = "565",
                    Comuna = new Comuna
                    {
                        name = "Independencia",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 7000,
                CuotaServicio = 350,
                Calificacion = 4.2
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Independencia",
                Direccion = new Direccion
                {
                    Calle = "Av. Independencia",
                    Numero = "864",
                    Comuna = new Comuna
                    {
                        name = "Independencia",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "35 - 45 minutos",
                Minimo = 8000,
                CuotaServicio = 450,
                Calificacion = 4.5
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Tobalaba",
                Direccion = new Direccion
                {
                    Calle = "Hernando de Aguirre",
                    Numero = "23",
                    Comuna = new Comuna
                    {
                        name = "Providencia",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 30 minutos",
                Minimo = 6000,
                CuotaServicio = 300,
                Calificacion = 4.2
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Macul",
                Direccion = new Direccion
                {
                    Calle = "Av. José Pedro Alessandri",
                    Numero = "1165",
                    Comuna = new Comuna
                    {
                        name = "Macul",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 6500,
                CuotaServicio = 350,
                Calificacion = 4.4
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Mall Plaza Norte",
                Direccion = new Direccion
                {
                    Calle = "Américo Vespucio Norte",
                    Numero = "1737",
                    Comuna = new Comuna
                    {
                        name = "Huechuraba",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 40 minutos",
                Minimo = 7500,
                CuotaServicio = 400,
                Calificacion = 4.3
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Kennedy",
                Direccion = new Direccion
                {
                    Calle = "Av. Kennedy",
                    Numero = "5055",
                    Comuna = new Comuna
                    {
                        name = "Las Condes",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 35 minutos",
                Minimo = 7000,
                CuotaServicio = 350,
                Calificacion = 4.5
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Mall Plaza Egaña",
                Direccion = new Direccion
                {
                    Calle = "Avda. Larraín",
                    Numero = "5862",
                    Comuna = new Comuna
                    {
                        name = "La Reina",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "35 - 45 minutos",
                Minimo = 8000,
                CuotaServicio = 400,
                Calificacion = 4.4
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Parque Arauco",
                Direccion = new Direccion
                {
                    Calle = "Av. Kennedy",
                    Numero = "5413",
                    Comuna = new Comuna
                    {
                        name = "Las Condes",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 40 minutos",
                Minimo = 7000,
                CuotaServicio = 400,
                Calificacion = 4.2
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's La Reina",
                Direccion = new Direccion
                {
                    Calle = "Príncipe de Gales",
                    Numero = "7252",
                    Comuna = new Comuna
                    {
                        name = "La Reina",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 6500,
                CuotaServicio = 350,
                Calificacion = 4.5
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Paseo Quilín",
                Direccion = new Direccion
                {
                    Calle = "Mar Tirreno",
                    Numero = "3349",
                    Comuna = new Comuna
                    {
                        name = "Peñalolén",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "20 - 30 minutos",
                Minimo = 6000,
                CuotaServicio = 300,
                Calificacion = 4.3
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Florida Center",
                Direccion = new Direccion
                {
                    Calle = "Av. Vicuña Mackenna",
                    Numero = "6100",
                    Comuna = new Comuna
                    {
                        name = "La Florida",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 6500,
                CuotaServicio = 350,
                Calificacion = 4.4
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Rotonda Atenas",
                Direccion = new Direccion
                {
                    Calle = "Tomás Moro",
                    Numero = "950",
                    Comuna = new Comuna
                    {
                        name = "Las Condes",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 30 minutos",
                Minimo = 6000,
                CuotaServicio = 300,
                Calificacion = 4.1
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Vitacura",
                Direccion = new Direccion
                {
                    Calle = "Av. Vitacura",
                    Numero = "7300",
                    Comuna = new Comuna
                    {
                        name = "Vitacura",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 40 minutos",
                Minimo = 7000,
                CuotaServicio = 350,
                Calificacion = 4.3
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Plaza Vespucio",
                Direccion = new Direccion
                {
                    Calle = "Av. Vicuña Mackenna Oriente",
                    Numero = "7110",
                    Comuna = new Comuna
                    {
                        name = "La Florida",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 35 minutos",
                Minimo = 7000,
                CuotaServicio = 350,
                Calificacion = 4.2
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Alto Las Condes",
                Direccion = new Direccion
                {
                    Calle = "Av. Kennedy",
                    Numero = "9001",
                    Comuna = new Comuna
                    {
                        name = "Las Condes",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "35 - 40 minutos",
                Minimo = 8000,
                CuotaServicio = 400,
                Calificacion = 4.4
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Plaza Italia",
                Direccion = new Direccion
                {
                    Calle = "Avda. Bernardo O’Higgins",
                    Numero = "68",
                    Comuna = new Comuna
                    {
                        name = "Providencia",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 6500,
                CuotaServicio = 350,
                Calificacion = 4.4
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Vicente Valdés",
                Direccion = new Direccion
                {
                    Calle = "Av. Vicuña Mackenna",
                    Numero = "8153",
                    Comuna = new Comuna
                    {
                        name = "La Florida",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 35 minutos",
                Minimo = 7000,
                CuotaServicio = 400,
                Calificacion = 4.3
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Maipú Mil",
                Direccion = new Direccion
                {
                    Calle = "Av. Pajaritos",
                    Numero = "1866",
                    Comuna = new Comuna
                    {
                        name = "Maipú",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "20 - 30 minutos",
                Minimo = 6000,
                CuotaServicio = 300,
                Calificacion = 4.2
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Gran Avenida Lider",
                Direccion = new Direccion
                {
                    Calle = "Av. José Miguel Carrera",
                    Numero = "6150",
                    Comuna = new Comuna
                    {
                        name = "San Miguel",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "35 - 45 minutos",
                Minimo = 7500,
                CuotaServicio = 400,
                Calificacion = 4.4
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Cantagallo",
                Direccion = new Direccion
                {
                    Calle = "Av. Las Condes",
                    Numero = "12207",
                    Comuna = new Comuna
                    {
                        name = "Las Condes",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 6500,
                CuotaServicio = 350,
                Calificacion = 4.3
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Portal La Dehesa",
                Direccion = new Direccion
                {
                    Calle = "Av. La Dehesa",
                    Numero = "1445",
                    Comuna = new Comuna
                    {
                        name = "Lo Barnechea",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "30 - 40 minutos",
                Minimo = 7500,
                CuotaServicio = 400,
                Calificacion = 4.5
            },
                    new Tienda
            {
                Cabecera = "https://i.ibb.co/Zm26z4f/banner-promocions.png",
                Logo = "https://i.ibb.co/QYcP1x6/logo1.png",
                Nombre = "McDonald's Mall Plaza Tobalaba",
                Direccion = new Direccion
                {
                    Calle = "Camilo Henríquez",
                    Numero = "3296",
                    Comuna = new Comuna
                    {
                        name = "Puente Alto",
                        Region = new Regiones
                        {
                            name = "Región Metropolitana",
                            Pais = new Pais
                            {
                                name = "Chile"
                            }
                        }
                    }
                },
                TiempoEntrega = "25 - 35 minutos",
                Minimo = 6500,
                CuotaServicio = 350,
                Calificacion = 4.3
            }
                };

                try
                {
                    foreach (var tienda in tiendas)
                    {
                        await DatabaseHelper.CreateDataAsync("Tiendas", tienda);
                        Console.WriteLine($"Tienda '{tienda.Nombre}' registrada correctamente.");
                    }

                    Console.WriteLine("Todas las tiendas han sido registradas exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al registrar tiendas: {ex.Message}");
                }
            }
        }
    }

}
