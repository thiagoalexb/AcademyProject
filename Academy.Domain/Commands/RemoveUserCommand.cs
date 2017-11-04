using Academy.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Domain.Commands
{
    public class RemoveUserCommand : UserCommand
    {
        public RemoveUserCommand(Guid id)
        {
            UserId = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
