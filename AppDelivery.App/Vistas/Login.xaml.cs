using DeliveryApp.Shared.Helpers;

namespace AppDelivery.App.Vistas;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        string email = entryUserName.Text;
        string password = entryPassword.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) 
        {
            await DisplayAlert("Error", "Por favor ngresa tu correo y tu contraseña", "OK");
            return;
        }

        try
        {
            bool isValid = await AuthHelper.LoginUserAsync(email, password);
            if (isValid)
            {
                await Navigation.PushAsync(new StoreView());
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void btnCrearCuenta_Clicked(object sender, EventArgs e)
    {

    }
}