
using DeliveryApp.Shared.Helpers;
using DeliveryApp.Shared.Modelos;
using LiteDB;
using System.Collections.ObjectModel;
using DeliveryApp.Shared.Enum;


namespace AppDelivery.App.Vistas;

public partial class StoreView : ContentPage
{

    #region variables
    public ObservableCollection<Tienda> Lista { get; set; } = new ObservableCollection<Tienda>();
    #endregion
    public StoreView()
	{
		InitializeComponent();
		CargarTiendas();
		BindingContext=this;
	}

    private async void CargarTiendas()
    {
		try
		{
			var tiendas = await DatabaseHelper.GetAllDataAsync<Tienda>(Variables.Tiendas.ToString());

            foreach (var tienda in tiendas) 
            {
                Lista.Add(new Tienda
                {
                     Key =tienda.Key,
                     Cabecera =tienda.Cabecera,
                     Logo =tienda.Logo,
                     Nombre=tienda.Nombre,    
                     TiempoEntrega =tienda.TiempoEntrega,
                     Direccion= tienda.Direccion,
                     Minimo = tienda.Minimo,
                     CuotaServicio=tienda.CuotaServicio,
                     Calificacion =tienda.Calificacion
                });
            }
            #region CodigoDeprecado
            //if (tiendas != null && tiendas.Any())
            //{
            //             TiendasList.ItemsSource = tiendas;
            //}
            //else
            //{
            //	await DisplayAlert("Informacion", "No se encontraron tiendas disponibles", "Ok");
            //}

            //         var empleados = await client
            //        .Child("Empleados")
            //        .OnceAsync<Empleado>();

            //         foreach (var empleado in empleadosActivos)
            //         {
            //             Lista.Add(new Empleado
            //             {
            //                 Id = empleado.Key,
            //                 PrimerNombre = empleado.Object.PrimerNombre,
            //                 SegundoNombre = empleado.Object.SegundoNombre,
            //                 PrimerApellido = empleado.Object.PrimerApellido,
            //                 SegundoApellido = empleado.Object.SegundoApellido,
            //                 CorreoElectronico = empleado.Object.CorreoElectronico,
            //                 Sueldo = empleado.Object.Sueldo,
            //                 FechaInicio = empleado.Object.FechaInicio,
            //                 Estado = empleado.Object.Estado,
            //                 Cargo = empleado.Object.Cargo
            //             });
            #endregion
        }
        catch (Exception)
		{

			throw;
		}
    }

    private void filtroSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        string filtro = filtroEntry.Text.ToLower();
        if (filtro.Length > 0)
        {
            TiendasList.ItemsSource = Lista.Where(x => x.Direccion.Comuna.name.ToLower().Contains(filtro)||x.Nombre.ToLower().Contains(filtro));
          
        }
        else
        {
            TiendasList.ItemsSource = Lista;
        }
    }
}