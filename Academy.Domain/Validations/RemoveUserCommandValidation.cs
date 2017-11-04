using Academy.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Domain.Validations
{
    public class RemoveUserCommandValidation : UserValidation<RemoveUserCommand>
    {
        public RemoveUserCommandValidation()
        {
            ValidateId();
        }
    }
}
