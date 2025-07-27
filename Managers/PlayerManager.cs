// In Managers/PlayerManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigTime.SDK.Managers
{
    public class PlayerManager
    {
        private readonly ApiClient _apiClient;

        // This is our cached data for flexible, non-async access.
        public PlayerProfile CurrentProfile { get; private set; }
        public List<InventoryItem> CurrentInventory { get; private set; }

        public PlayerManager(ApiClient client)
        {
            _apiClient = client;
            CurrentInventory = new List<InventoryItem>();
        }

        /// <summary>
        /// Fetches the profile details of the currently authenticated user from the server
        /// and stores it for easy access via `CurrentProfile`.
        /// </summary>
        public async Task<PlayerProfile> GetProfileAsync()
        {
            CurrentProfile = await _apiClient.GetAsync<PlayerProfile>("auth/me/");
            return CurrentProfile;
        }

        /// <summary>
        /// Fetches the inventory items of the currently authenticated user from the server
        /// and stores it for easy access via `CurrentInventory`.
        /// </summary>
        public async Task<List<InventoryItem>> GetInventoryAsync()
        {
            CurrentInventory = await _apiClient.GetAsync<List<InventoryItem>>("inventory/user/");
            return CurrentInventory;
        }
    }
}