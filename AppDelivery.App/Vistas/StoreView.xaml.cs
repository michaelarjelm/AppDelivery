
using DeliveryApp.Shared.Helpers;
using DeliveryApp.Shared.Modelos;

namespace AppDelivery.App.Vistas;

public partial class StoreView : ContentPage
{
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
			var tiendas = await DatabaseHelper.GetAllDataAsync<Tienda>("Tiendas");
			if (tiendas != null && tiendas.Any())
			{
                TiendasList.ItemsSource = tiendas;
			}
			else
			{
				await DisplayAlert("Informacion", "No se encontraron tiendas disponibles", "Ok");
			}
			
		}
		catch (Exception)
		{

			throw;
		}
    }
}