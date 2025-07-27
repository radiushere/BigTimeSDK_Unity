// In Models/Friends.cs
using Newtonsoft.Json;
using System;

namespace BigTime.SDK.Models
{
    public class Friend
    {
        // This assumes the friend list returns user profile-like objects
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("username")]
        public string Username;
    }

    public class FriendRequest
    {
        [JsonProperty("id")]
        public Guid Id; // The ID is a Guid (e.g., "da567f85-...")

        [JsonProperty("from_user")]
        public Friend Sender;

        [JsonProperty("to_user")]
        public Friend Receiver;

        [JsonProperty("created_at")]
        public DateTime CreatedAt;
    }
}