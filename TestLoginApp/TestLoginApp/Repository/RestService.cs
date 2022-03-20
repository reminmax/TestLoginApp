using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestLoginApp.Helpers;
using TestLoginApp.Models;

namespace TestLoginApp.Repository
{
    public class RestService : IRestService
    {
        private HttpClient _httpClient;
        private readonly Uri _baseUri = new Uri(ConstantValues.BaseUri);

        public RestService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler()
            {
                // added to avoid "The SSL connection could not be established" error
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _httpClient = new HttpClient(clientHandler);

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseLoginModel> LoginAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException(nameof(login));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            var builder = new UriBuilderExt(string.Concat(_baseUri, ConstantValues.LoginUri));
            var uri = builder.Uri;

            try
            {
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(login), "\"phone_or_mail\"");
                    formData.Add(new StringContent(password), "\"password\"");

                    var response = await _httpClient.PostAsync(uri, formData);
                    response.EnsureSuccessStatusCode();

                    string content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<HttpResponseLoginModel>(content);

                    return result;
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseLoginModel()
                { 
                    Error = ex.Message
                };
            }
        }

        public async Task<HttpResponseProfileModel> GetUserProfileAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            var builder = new UriBuilderExt(string.Concat(_baseUri, ConstantValues.ProfileUri));
            var uri = builder.Uri;

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsync(uri, null);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<HttpResponseProfileModel>(content);

                return result;
            }
            catch (Exception ex)
            {

                return new HttpResponseProfileModel()
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<bool> LogoutAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            var builder = new UriBuilderExt(string.Concat(_baseUri, ConstantValues.ProfileUri));
            var uri = builder.Uri;

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsync(uri, null);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
