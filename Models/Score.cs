// In Assets/BigTime-SDK/Models/Score.cs
using Newtonsoft.Json;

namespace BigTime.SDK.Models
{
    public class ScoreSubmissionResult
    {
        [JsonProperty("detail")]
        public string Detail; // e.g., "Score recorded"
    }
}