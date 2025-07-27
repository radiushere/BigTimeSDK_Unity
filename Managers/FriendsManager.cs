// In Managers/FriendsManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigTime.SDK.Managers
{
    public class FriendsManager
    {
        private readonly ApiClient _apiClient;
        public FriendsManager(ApiClient client) { _apiClient = client; }

        /// <summary>
        /// Gets the player's current list of friends.
        /// </summary>
        public Task<List<Friend>> GetFriendsAsync()
        {
            return _apiClient.GetAsync<List<Friend>>("friends/");
        }

        /// <summary>
        /// Gets all pending incoming friend requests for the player.
        /// </summary>
        public Task<List<FriendRequest>> GetPendingRequestsAsync()
        {
            return _apiClient.GetAsync<List<FriendRequest>>("friends/requests/");
        }

        /// <summary>
        /// Sends a friend request to another user by their username.
        /// </summary>
        public Task<FriendRequest> SendFriendRequestAsync(string username)
        {
            var payload = new { to_username = username };
            return _apiClient.PostAsync<FriendRequest>("friends/requests/", payload);
        }

        /// <summary>
        /// Accepts a friend request using its unique ID.
        /// </summary>
        public Task<WalletTransactionResult> AcceptFriendRequestAsync(Guid requestId)
        {
            return _apiClient.PostAsync<WalletTransactionResult>($"friends/requests/{requestId}/accept/", null);
        }

        /// <summary>
        /// Declines a friend request using its unique ID.
        /// </summary>
        public Task<WalletTransactionResult> DeclineFriendRequestAsync(Guid requestId)
        {
            return _apiClient.PostAsync<WalletTransactionResult>($"friends/requests/{requestId}/decline/", null);
        }

        /// <summary>
        /// Removes a friend from the friends list by their username.
        /// </summary>
        public Task<WalletTransactionResult> UnfriendAsync(string username)
        {
            return _apiClient.PostAsync<WalletTransactionResult>($"friends/unfriend/{username}/", null);
        }
    }
}