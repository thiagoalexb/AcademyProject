using Academy.Domain.Commands;

namespace Academy.Domain.Validations
{
    public class UpdateUserPasswordCommandVailidation : UserValidation<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandVailidation()
        {
            ValidatePassword();
            ValidateId();
        }
    }
}
