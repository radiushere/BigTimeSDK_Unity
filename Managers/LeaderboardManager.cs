// In Assets/BigTime-SDK/Managers/LeaderboardManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System.Threading.Tasks;

namespace BigTime.SDK.Managers
{
    public enum LeaderboardPeriod { daily, weekly, monthly }

    public class LeaderboardManager
    {
        private readonly ApiClient _apiClient;
        public LeaderboardManager(ApiClient client) { _apiClient = client; }

        // NOTE: The SubmitScoreAsync function has been moved to ScoreManager.

        /// <summary>
        /// Gets the top leaderboard entries for a given period.
        /// </summary>
        public Task<Leaderboard> GetTopScoresAsync(LeaderboardPeriod period, int limit = 10, int offset = 0)
        {
            string periodString = period.ToString().ToLower();
            string endpoint = $"leaderboard/?limit={limit}&period={periodString}&offset={offset}";
            return _apiClient.GetAsync<Leaderboard>(endpoint);
        }

        /// <summary>
        /// Gets the personal rank of the current player for a given period.
        /// </summary>
        public Task<UserRank> GetMyRankAsync(LeaderboardPeriod period)
        {
            string periodString = period.ToString().ToLower();
            string endpoint = $"leaderboard/me/?period={periodString}";
            return _apiClient.GetAsync<UserRank>(endpoint);
        }
    }
}