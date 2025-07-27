// In BigTimeSDK.cs
using BigTime.SDK.Core;
using BigTime.SDK.Managers;
using BigTime.SDK.Utilities;

namespace BigTime.SDK
{
    public static class BigTimeSDK
    {
        // Public-facing managers for the game developer to use
        public static PlayerManager Player { get; private set; }
        public static AvatarManager Avatar { get; private set; }
        public static LeaderboardManager Leaderboard { get; private set; }
        public static WalletManager Wallet { get; private set; }
        public static RewardsManager Rewards { get; private set; }
        public static FriendsManager Friends { get; private set; }
        public static ReportsManager Reports { get; private set; }
        public static ScoreManager Score { get; private set; }

        private static bool _isInitialized = false;

        public static void Initialize(string accessToken, string refreshToken)
        {
            if (_isInitialized)
            {
                UnityEngine.Debug.LogWarning("BigTime SDK already initialized.");
                return;
            }

            string baseUri = "http://167.71.255.240:8000/";

            MainThreadDispatcher.Initialize();

            var authManager = new AuthManager(baseUri);
            authManager.SetTokens(accessToken, refreshToken);

            var apiClient = new ApiClient(baseUri, authManager);

            // Create and expose all feature managers
            Player = new PlayerManager(apiClient);
            Avatar = new AvatarManager(apiClient);
            Leaderboard = new LeaderboardManager(apiClient);
            Wallet = new WalletManager(apiClient);
            Rewards = new RewardsManager(apiClient);
            Friends = new FriendsManager(apiClient);
            Reports = new ReportsManager(apiClient);
            Score = new ScoreManager(apiClient);

            _isInitialized = true;
            UnityEngine.Debug.Log("BigTime SDK Initialized Successfully.");
        }
    }
}