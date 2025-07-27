// In Assets/BigTime-SDK/Managers/ScoreManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System.Threading.Tasks;

namespace BigTime.SDK.Managers
{
    public class ScoreManager
    {
        private readonly ApiClient _apiClient;
        public ScoreManager(ApiClient client) { _apiClient = client; }

        /// <summary>
        /// Submits a new score for the player to a specific category.
        /// </summary>
        /// <param name="points">The number of points to add.</param>
        /// <param name="category">The category, e.g., "daily", "weekly".</param>
        public Task<ScoreSubmissionResult> SubmitScoreAsync(int points, string category)
        {
            var payload = new { points, category };
            return _apiClient.PostAsync<ScoreSubmissionResult>("leaderboard/scores/", payload);
        }
    }
}