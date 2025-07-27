// In Models/Rewards.cs
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BigTime.SDK.Models
{
    public class Achievement
    {
        [JsonProperty("code")]
        public string Code;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("is_completed")]
        public bool IsCompleted;

        [JsonProperty("is_claimed")]
        public bool IsClaimed;

        // This is a "helper property" that makes the SDK easier to use.
        public bool IsClaimable => IsCompleted && !IsClaimed;
    }

    public class TierProgress
    {
        // Add properties based on the /rewards/tiers/ response when available
    }
}