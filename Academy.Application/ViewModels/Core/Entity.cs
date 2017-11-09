using System;

namespace Academy.Application.ViewModels.Core
{
    public abstract class Entity
    {
        public DateTime CreationDate { get; set; }
        public Guid? CreatorUserId { get; set; }

        public DateTime? LastUpdateDate { get; set; }
        public Guid? LastUpdatedUserId { get; set; }
    }
}
