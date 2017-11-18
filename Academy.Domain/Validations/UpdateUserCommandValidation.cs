using Academy.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Domain.Validations
{

    public class UpdateUserCommandValidation : UserValidation<UpdateUserCommand>
    {
        public UpdateUserCommandValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidateBirthDate();
            ValidateId();
        }
    }
}
