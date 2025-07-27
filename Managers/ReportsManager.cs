// In Managers/ReportsManager.cs
using BigTime.SDK.Core;
using BigTime.SDK.Models;
using System.Threading.Tasks;

namespace BigTime.SDK.Managers
{
    public class ReportsManager
    {
        private readonly ApiClient _apiClient;
        public ReportsManager(ApiClient client) { _apiClient = client; }

        /// <summary>
        /// Reports a piece of content, like a message or a user profile.
        /// </summary>
        /// <param name="model">The type of item being reported (e.g., "messaging.message")</param>
        /// <param name="objectId">The unique ID of the specific item being reported</param>
        /// <param name="reason">The reason for the report (e.g., "Spam")</param>
        public Task<ReportResult> SubmitReportAsync(string model, string objectId, string reason)
        {
            var payload = new { model, object_id = objectId, reason };
            return _apiClient.PostAsync<ReportResult>("/reports/", payload);
        }
    }
}