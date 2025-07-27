// In Models/Reports.cs
using Newtonsoft.Json;

namespace BigTime.SDK.Models
{
    public class ReportResult
    {
        [JsonProperty("detail")]
        public string Detail;
    }
}