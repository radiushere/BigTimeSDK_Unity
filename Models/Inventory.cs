using Newtonsoft.Json; 
using System.Collections.Generic;

namespace BigTime.SDK.Models
{
    public class ItemDetails
    {
        [JsonProperty("code")]
        public string Code;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;
    }

    public class InventoryItem
    {
        [JsonProperty("item")]
        public ItemDetails Item;

        [JsonProperty("quantity")]
        public int Quantity;
    }
}