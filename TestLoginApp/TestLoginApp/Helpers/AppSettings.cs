using Xamarin.Essentials;

namespace TestLoginApp.Helpers
{
    public static class AppSettings
    {
        public static string Token
        {
            get
            {
                if (Preferences.ContainsKey("Token"))
                    return Preferences.Get("Token", string.Empty);
                else
                    return string.Empty;
            }
            set => Preferences.Set("Token", value);
        }

    }
}
