// In Models/Wallet.cs
using Newtonsoft.Json;

namespace BigTime.SDK.Models
{
    // For handling generic wallet transactions
    public class WalletTransactionResult
    {
        [JsonProperty("detail")]
        public string Detail;
    }

    // For specifically getting the balance
    public class WalletBalance
    {
        [JsonProperty("balance")]
        public float Balance;
    }
}