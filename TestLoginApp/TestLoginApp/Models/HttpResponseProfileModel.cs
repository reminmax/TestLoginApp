using Newtonsoft.Json;

namespace TestLoginApp.Models
{
    public class HttpResponseProfileModel : HttpResponseModelBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
