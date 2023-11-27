namespace Order.Broker.Models
{
    public class EventProperties
    {
        public string SpecVersion { get; set; } = "1.0";

        public string Type { get; set; } = String.Empty;

        public string Source { get; set; } = String.Empty;

        public string Subject { get; set; } = String.Empty;

        public string Id { get; set; } = String.Empty;
    }
}
