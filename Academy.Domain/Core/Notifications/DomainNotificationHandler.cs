using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Domain.Core.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Handle(DomainNotification message)
        {
            _notifications.Add(message);
        }

        public virtual List<DomainNotification> GetNotifications() =>
            _notifications;

        public virtual bool HasNotifications() =>
            GetNotifications().Any();

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}
