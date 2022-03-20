using Newtonsoft.Json;

namespace TestLoginApp.Models
{
    public class HttpResponseLoginModel : HttpResponseModelBase
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
