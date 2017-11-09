using Academy.Domain.Core.Commands;
using System;

namespace Academy.Domain.Entities.Core
{
    public abstract class EntityCommand : Command
    {
        public DateTime CreationDate { get; set; }
        public Guid? CreatorUserId { get; set; }

        public DateTime? LastUpdateDate { get; set; }
        public Guid? LastUpdatedUserId { get; set; }
    }
}
