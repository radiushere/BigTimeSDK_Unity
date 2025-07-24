// In Assets/BigTime-SDK/Core/AuthManager.cs
using BigTime.SDK.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BigTime.SDK.Core
{
    public class AuthManager
    {
        public string AccessToken { get; private set; }
        private string RefreshToken { get; set; }
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public AuthManager(string baseUri)
        {
            _baseUri = baseUri;
            _httpClient = new HttpClient { BaseAddress = new System.Uri(_baseUri) };
        }

        public void SetTokens(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public async Task<bool> RefreshTokenAsync()
        {
            UnityEngine.Debug.Log("<color=orange>ACCESS TOKEN EXPIRED. Attempting to refresh...</color>");

            if (string.IsNullOrEmpty(RefreshToken))
            {
                UnityEngine.Debug.LogError("<color=red>Auth Error: No Refresh Token available to use.</color>");
                return false;
            }

            var requestData = new { refresh = RefreshToken };
            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("auth/token/refresh/", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                UnityEngine.Debug.LogError($"<color=red>TOKEN REFRESH FAILED. Status: {response.StatusCode}. Response: {responseContent}</color>");
                AccessToken = null;
                RefreshToken = null;
                return false;
            }

            UnityEngine.Debug.Log($"<color=green>TOKEN REFRESH SUCCEEDED. Response: {responseContent}</color>");
            var newTokens = JsonConvert.DeserializeObject<AuthTokens>(responseContent);

            AccessToken = newTokens.access;

            if (!string.IsNullOrEmpty(newTokens.refresh))
            {
                RefreshToken = newTokens.refresh;
                UnityEngine.Debug.Log("<color=cyan>A new Refresh Token was issued and has been updated.</color>");
            }

            return true;
        }
    }
}