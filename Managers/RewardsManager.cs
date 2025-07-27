// In Managers/RewardsManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigTime.SDK.Managers
{
    public class RewardsManager
    {
        private readonly ApiClient _apiClient;
        public RewardsManager(ApiClient client) { _apiClient = client; }

        /// <summary>
        /// Gets the status of all achievements for the current player.
        /// </summary>
        public Task<List<Achievement>> GetAchievementsAsync()
        {
            return _apiClient.GetAsync<List<Achievement>>("rewards/achievements/");
        }

        /// <summary>
        /// Claims the reward for a specific achievement.
        /// </summary>
        public Task<WalletTransactionResult> ClaimAchievementAsync(string achievementCode)
        {
            // The response for a claim is often a simple "detail" message.
            return _apiClient.PostAsync<WalletTransactionResult>($"rewards/achievements/{achievementCode}/claim/", null);
        }
    }
}