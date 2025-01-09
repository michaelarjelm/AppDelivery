using Firebase.Auth;
using Firebase.Auth.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Shared.Helpers
{
    public static class AuthHelper
    {
        private static FirebaseAuthClient _authClient;

        static AuthHelper()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyADODyaQTWsLHtySBNUsdGeSHOZoEmG_0Y",
                AuthDomain = "appdelivery-4a23a.firebaseapp.com",
                Providers = new[]
                {
                    new EmailProvider()
                }
            };
            _authClient = new FirebaseAuthClient(config);
        }

        public static async Task<bool> LoginUserAsync(string email, string password)
        {
            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
       
    }
}
