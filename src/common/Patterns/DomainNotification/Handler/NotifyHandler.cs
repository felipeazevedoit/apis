using TServices.Comum.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TServices.Comum.Patterns.DomainNotification.Handler
{
    public class NotifyHandler : MediatR.INotificationHandler<DomainNotifications>
    {
        private List<DomainNotifications> _notifications;

        public NotifyHandler() => _notifications = new List<DomainNotifications>();

        public Task Handle(DomainNotifications message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotifications> GetNotifications()
        {
            return _notifications.Where(not =>
                not.GetType() == typeof(DomainNotifications)).ToList();
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotifications>();
        }
    }
}
