// In Assets/BigTime-SDK/Managers/PlayerManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigTime.SDK.Managers
{
    public class PlayerManager
    {
        private readonly ApiClient _apiClient;
        public PlayerManager(ApiClient client) { _apiClient = client; }

        public Task<PlayerProfile> GetProfileAsync()
        {
            return _apiClient.GetAsync<PlayerProfile>("auth/me/");
        }

        public Task<List<InventoryItem>> GetInventoryAsync()
        {
            return _apiClient.GetAsync<List<InventoryItem>>("inventory/user/");
        }
    }
}