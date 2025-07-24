// In Assets/BigTime-SDK/Models/Avatar.cs
using System.Collections.Generic;

namespace BigTime.SDK.Models
{
    public class AvatarConfig
    {
        public string hair;
        public string outfit;
        public Dictionary<string, string> equipped; 
    }

    public class AvatarData
    {
        public AvatarConfig config;
    }
}