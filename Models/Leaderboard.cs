using Newtonsoft.Json; 
using System.Collections.Generic;

namespace BigTime.SDK.Models
{
    public class LeaderboardEntry
    {
        [JsonProperty("rank")]
        public int Rank;

        [JsonProperty("user__username")]
        public string Username;

        [JsonProperty("total")] 
        public int Points;
    }

    public class Leaderboard
    {
        [JsonProperty("leaders")]
        public List<LeaderboardEntry> Results = new List<LeaderboardEntry>();
    }

    public class UserRank
    {
        [JsonProperty("rank")]
        public int Rank;

        [JsonProperty("total")] 
        public int Points;
    }
}