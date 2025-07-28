// In Assets/BigTime-SDK/Managers/WalletManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System.Threading.Tasks;
using UnityEngine; // For Debug.LogWarning

namespace BigTime.SDK.Managers
{
    public class WalletManager
    {
        private readonly ApiClient _apiClient;

        // --- NEW: Exchange Rate Logic ---
        private float _exchangeRate = 1.0f; // Default to 1, meaning 1 in-game unit = 1 Token.
        private float _cachedTokenBalance = 0.0f;

        public WalletManager(ApiClient client) { _apiClient = client; }

        /// <summary>
        /// Sets the conversion rate for in-game currency. This should be called once after SDK initialization.
        /// </summary>
        /// <param name="rate">How many units of your in-game currency equal 1 official BigTime Token.</param>
        public void SetExchangeRate(float rate)
        {
            if (rate <= 0)
            {
                Debug.LogWarning("Exchange rate must be greater than 0. Defaulting to 1.");
                _exchangeRate = 1.0f;
                return;
            }
            _exchangeRate = rate;
        }

        /// <summary>
        /// Gets the player's current wallet balance in the official Token currency.
        /// Also updates the local cache.
        /// </summary>
        public async Task<float> GetBalanceInTokensAsync()
        {
            var result = await _apiClient.GetAsync<WalletBalance>("wallet/balance/");
            _cachedTokenBalance = result.Balance;
            return _cachedTokenBalance;
        }

        /// <summary>
        /// Gets the player's balance, automatically converted to your defined in-game currency.
        /// </summary>
        public async Task<int> GetBalanceInGameCurrencyAsync()
        {
            // First, make sure we have the latest token balance from the server.
            await GetBalanceInTokensAsync();
            // Now, perform the conversion.
            return (int)(_cachedTokenBalance * _exchangeRate);
        }

        /// <summary>
        /// [ADVANCED] A generic function to perform a transaction using the official Token currency.
        /// </summary>
        public Task<WalletTransactionResult> PerformTransactionAsync(string action, float amountInTokens, string recipient = null)
        {
            var payload = new { action, amount = amountInTokens.ToString("F2"), recipient };
            return _apiClient.PostAsync<WalletTransactionResult>("wallet/transaction/", payload);
        }

        /// <summary>
        /// Rewards the player with a specific amount of your IN-GAME currency.
        /// The SDK handles the conversion to official Tokens automatically.
        /// </summary>
        public Task<WalletTransactionResult> RewardInGameCurrencyAsync(int amountInGameCurrency)
        {
            float amountInTokens = (float)amountInGameCurrency / _exchangeRate;
            return PerformTransactionAsync("add", amountInTokens);
        }

        /// <summary>
        /// Deducts a specific amount of your IN-GAME currency from the player's wallet.
        /// The SDK handles the conversion to official Tokens automatically.
        /// </summary>
        public Task<WalletTransactionResult> DeductInGameCurrencyAsync(int amountInGameCurrency)
        {
            float amountInTokens = (float)amountInGameCurrency / _exchangeRate;
            return PerformTransactionAsync("deduct", amountInTokens);
        }
    }
}