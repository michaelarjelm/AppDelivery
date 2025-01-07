using AppDelivery.App.Vistas;

namespace AppDelivery.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Login();
        }
    }
}
