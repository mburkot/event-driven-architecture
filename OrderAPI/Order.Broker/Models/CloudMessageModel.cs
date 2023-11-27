using System.Text.Json;
using System.Text.Json.Serialization;

namespace Order.Broker.Models
{
    public class CloudMessageModel<T>
    {
        [JsonPropertyName("specversion")]
        public string SpecVersion { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("datacontenttype")]
        public string DataContentType { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        public static CloudMessageModel<T> Create<T>(T message, EventProperties eventProperties)
        {
            return new CloudMessageModel<T>()
            {
                SpecVersion = eventProperties.SpecVersion,
                Type = eventProperties.Type,
                Source = eventProperties.Source,
                Subject = eventProperties.Subject,
                Id = eventProperties.Id,
                Time = DateTime.UtcNow,
                DataContentType = "application/json",
                Data = message
            };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}