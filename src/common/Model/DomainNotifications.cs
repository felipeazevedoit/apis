using MediatR;
using Newtonsoft.Json;

namespace TServices.Comum.Model
{ 
    public class DomainNotifications : INotification
    {
        [JsonProperty(Order = -2)]
        public string Key { get; }

        [JsonProperty(Order = -1)]
        public string Value { get; }

        public DomainNotifications(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
