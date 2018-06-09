using Newtonsoft.Json;

namespace exact.api.Model
{
    public class GenericAtribute
    {
        
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("value")]
        public double Value { get; set; }
    }
}