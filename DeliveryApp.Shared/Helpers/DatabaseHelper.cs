using DeliveryApp.Shared.Modelos;
using Firebase.Database;
using Firebase.Database.Query;

namespace DeliveryApp.Shared.Helpers
{
    public static class DatabaseHelper
    {
        private static readonly FirebaseClient _firebaseClient = new FirebaseClient("https://appdelivery-4a23a-default-rtdb.firebaseio.com/");

        public static async Task<string> CreateDataAsync<T>(string path, T data)
        {
            var result = await _firebaseClient.Child(path).PostAsync(data);
            return result.Key;
        }

        public static async Task UpdateDataAsync<T>(string path, T data)
        {
            await _firebaseClient.Child(path).PutAsync(data);
        }

        public static async Task<T?> GetDataAsync<T>(string path)
        {
            var data = await _firebaseClient.Child(path).OnceSingleAsync<T>();
            return data;
        }

        public static async Task<List<T>> GetAllDataAsync<T>(string path) where T : class, new()
        {
            var data = await _firebaseClient.Child(path).OnceAsync<T>();

            foreach (var item in data)
            {
                var obj = item.Object;
                var keyProperty = typeof(T).GetProperty("Key");

                if (keyProperty != null && keyProperty.CanWrite)
                {
                    keyProperty.SetValue(obj, item.Key);
                }
            }
            return data.Select(item => item.Object).ToList();

        }

        public static async Task<T?> GetDataByEmailAsync<T>(string node, string email) where T : class, new()
        {
            try
            {
                var allData = await GetAllDataAsync<T>(node);
                return allData?.FirstOrDefault(data => (data as Usuario)?.CorreoElectronico == email);
            }
            catch
            {
                return default;
            }
        }
    }
}
