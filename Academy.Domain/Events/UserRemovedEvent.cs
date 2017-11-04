using Academy.Domain.Core.Events;
using System;

namespace Academy.Domain.Events
{
    public class UserRemovedEvent : Event
    {
        public UserRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
