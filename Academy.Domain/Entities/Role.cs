using Academy.Domain.Entities.Core;
using System;

namespace Academy.Domain.Entities
{
    public class Role : Entity
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
    }
}
