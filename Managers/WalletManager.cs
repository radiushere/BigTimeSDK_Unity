// In Managers/WalletManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System.Threading.Tasks;

namespace BigTime.SDK.Managers
{
    public class WalletManager
    {
        private readonly ApiClient _apiClient;
        public WalletManager(ApiClient client) { _apiClient = client; }

        /// <summary>
        /// Gets the player's current wallet balance directly from the server.
        /// </summary>
        public async Task<float> GetBalanceAsync()
        {
            var result = await _apiClient.GetAsync<WalletBalance>("wallet/balance/");
            return result.Balance;
        }

        /// <summary>
        /// A generic function to perform a transaction. For game use, this would typically be "deduct".
        /// Note: For an in-game store, it's better to have a dedicated purchase endpoint.
        /// This function is for direct token manipulation.
        /// </summary>
        public Task<WalletTransactionResult> PerformTransactionAsync(string action, float amount, string recipient = null)
        {
            var payload = new { action, amount = amount.ToString("F2"), recipient };
            return _apiClient.PostAsync<WalletTransactionResult>("wallet/transaction/", payload);
        }
    }
}