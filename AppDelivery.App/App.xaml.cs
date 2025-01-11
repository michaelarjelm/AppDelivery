using AppDelivery.App.Vistas;
using DeliveryApp.Shared.SeedDB;

namespace AppDelivery.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                try
                {
                    await DataLoadHelper.ActualizarPaisesRegionesYComunas();
                    await DataLoadHelper.RegistrarTiendas();
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error al actualizar los datos: {ex.Message}" );
                }

            });       
            

            MainPage = new NavigationPage(new 
                Login());
        }
    }
}
