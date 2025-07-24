// In Assets/BigTime-SDK/BigTimeSDK.cs
using BigTime.SDK.Core;
using BigTime.SDK.Managers;
using BigTime.SDK.Utilities;

namespace BigTime.SDK
{
    public static class BigTimeSDK
    {
        public static PlayerManager Player { get; private set; }
        public static AvatarManager Avatar { get; private set; }
        public static LeaderboardManager Leaderboard { get; private set; }

        private static bool _isInitialized = false;

        /// <param name="accessToken">The player's access token provided by the game hub.</param>
        /// <param name="refreshToken">The player's refresh token provided by the game hub.</param>
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

            Player = new PlayerManager(apiClient);
            Avatar = new AvatarManager(apiClient);
            Leaderboard = new LeaderboardManager(apiClient);

            _isInitialized = true;
            UnityEngine.Debug.Log("BigTime SDK Initialized Successfully.");
        }
    }
}