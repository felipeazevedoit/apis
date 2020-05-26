using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using TServices.Comum.Patterns.DomainNotification.Interface;
using TServices.Comum.Patterns.DomainNotification.Handler;
using TServices.Comum.Model;

namespace TServices.Comum.Patterns.DomainNotification.Implementation
{
    public class Notify : INotify
    {
        private readonly NotifyHandler _messageHandler;

        public Notify(INotificationHandler<DomainNotifications> notification)
        {
            _messageHandler = (NotifyHandler)notification;
        }

        public Notify Invoke()
        {
            return this;
        }

        public bool IsValid()
        {
            return !_messageHandler.HasNotifications();
        }

        public void NewNotification(string key, string message)
        {
            _messageHandler.Handle(new DomainNotifications(key, message), default(CancellationToken));
        } 
        
        public void NewNotification(Dictionary<string, string> dictionaryMessage)
        {
            if (dictionaryMessage.Count == 0) return;

            dictionaryMessage.ToList().ForEach(x =>
            {
                _messageHandler.Handle(new DomainNotifications(x.Key, x.Value), default(CancellationToken));
            });
        }
    }
}
