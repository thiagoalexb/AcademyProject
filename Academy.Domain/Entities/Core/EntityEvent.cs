using Academy.Domain.Core.Events;
using System;

namespace Academy.Domain.Entities.Core
{
    public abstract class EntityEvent : Event
    {
        public DateTime CreationDate { get; set; }
        public Guid? CreatorUserId { get; set; }

        public DateTime? LastUpdateDate { get; set; }
        public Guid? LastUpdatedUserId { get; set; }
    }
}
