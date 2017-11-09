using Academy.Domain.Entities.Core;
using System;

namespace Academy.Domain.Entities
{
    public class Tag : Entity
    {
        public Guid TagId { get; set; }
        public string Name { get; set; }
    }
}
