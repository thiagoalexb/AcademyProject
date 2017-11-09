using Academy.Domain.Core.Commands;
using Academy.Domain.Entities.Core;
using System;

namespace Academy.Domain.Commands
{
    public abstract class UserCommand : EntityCommand
    {
        public Guid UserId { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
    }
}
