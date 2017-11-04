using Academy.Domain.Commands;

namespace Academy.Domain.Validations
{
    public class RegisterNewUserCommandValidation : UserValidation<RegisterNewUserCommand>
    {
        public RegisterNewUserCommandValidation()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidateBirthDate();
            ValidatePassword();
        }
    }
}
