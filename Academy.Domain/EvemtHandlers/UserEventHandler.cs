using Academy.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Domain.EvemtHandlers
{
    public class UserEventHandler :
        INotificationHandler<UserRegisteredEvent>,
        INotificationHandler<UserUpdatedEvent>,
        INotificationHandler<UserRemovedEvent>
    {
        public void Handle(UserUpdatedEvent message)
        {
            // Send some notification e-mail
        }

        public void Handle(UserRegisteredEvent message)
        {
            // Send some greetings e-mail
        }

        public void Handle(UserRemovedEvent message)
        {
            // Send some see you soon e-mail
        }
    }
}
