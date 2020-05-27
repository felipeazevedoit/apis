using System;
using System.Collections.Generic;
using System.Text;

namespace TServices.Comum.Model.Notificacao
{
    public class MessageQueue
    {
        public MessageQueue(string url, string key, object model, bool durable = false, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            Url = url;
            Key = string.IsNullOrWhiteSpace(key) ? Guid.NewGuid().ToString() : key;
            Model = model;
            Durable = durable;
            Exclusive = exclusive;
            AutoDelete = autoDelete;
            Arguments = arguments;
        }

        public string Url { get; private set; }
        public string Key { get; private set; }
        public object Model { get; private set; }
        public bool Durable { get; private set; }
        public bool Exclusive { get; private set; }
        public bool AutoDelete { get; private set; }
        public IDictionary<string, object> Arguments { get; private set; }
        public Uri Uri => new Uri(Url.Replace("amqp://", "amqps://"));
    }
}
