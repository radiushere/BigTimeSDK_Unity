// In Assets/BigTime-SDK/Managers/AvatarManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System.Threading.Tasks;

namespace BigTime.SDK.Managers
{
    public class AvatarManager
    {
        private readonly ApiClient _apiClient;
        public AvatarManager(ApiClient client) { _apiClient = client; }

        public Task<AvatarData> GetConfigAsync()
        {
            return _apiClient.GetAsync<AvatarData>("avatar/config/");
        }

        public Task<AvatarData> UpdateConfigAsync(AvatarConfig newConfig)
        {
            var payload = new { config = newConfig };
            return _apiClient.PostAsync<AvatarData>("avatar/config/", payload);
        }
    }
}