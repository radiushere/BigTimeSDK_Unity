using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BigTime.SDK.Core
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthManager _authManager;
        private readonly string _baseUri;

        public ApiClient(string baseUri, AuthManager authManager)
        {
            _baseUri = baseUri;
            _authManager = authManager;
            _httpClient = new HttpClient { BaseAddress = new System.Uri(_baseUri) };
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            return await SendRequestAsync<T>(HttpMethod.Get, endpoint, null);
        }

        public async Task<TResponse> PostAsync<TResponse>(string endpoint, object data)
        {
            return await SendRequestAsync<TResponse>(HttpMethod.Post, endpoint, data);
        }

        private async Task<T> SendRequestAsync<T>(HttpMethod method, string endpoint, object data = null)
        {
            if (string.IsNullOrEmpty(_authManager.AccessToken))
            {
                throw new System.Exception("SDK not initialized or user not logged in.");
            }

            var request = new HttpRequestMessage(method, endpoint);

            if (data != null)
            {
                var json = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authManager.AccessToken);

            var response = await _httpClient.SendAsync(request);

            UnityEngine.Debug.Log($"Request to {endpoint} returned status code: {response.StatusCode}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                bool refreshed = await _authManager.RefreshTokenAsync();
                if (refreshed)
                {
                    UnityEngine.Debug.Log("<color=cyan>Retrying original request with new token...</color>");
                    var retryRequest = new HttpRequestMessage(method, endpoint);
                    if (data != null)
                    {
                        retryRequest.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    }
                    retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authManager.AccessToken);
                    response = await _httpClient.SendAsync(retryRequest);
                }
            }

            var content = await response.Content.ReadAsStringAsync();

            UnityEngine.Debug.Log($"<color=lime>RAW RESPONSE for {endpoint}:</color> {content}");

            if (!response.IsSuccessStatusCode)
            {
                UnityEngine.Debug.LogError($"API Error after retry attempt. Status: {response.StatusCode}. Content: {content}");
            }
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}