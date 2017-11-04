using Academy.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Domain.Commands
{
    public abstract class UserCommand : Command
    {
        public Guid UserId { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
    }
}
