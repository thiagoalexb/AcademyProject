using Academy.Domain.Validations;
using System;

namespace Academy.Domain.Commands
{
    public class RegisterNewUserCommand : UserCommand
    {
        public RegisterNewUserCommand(string firstName, string lastName, string email, string password, DateTime dateOfBirth,
                                        DateTime creationDate, Guid? creatorUserId, DateTime? lastUpdateDate, Guid? lastUpdatedUserId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            CreationDate = creationDate;
            CreatorUserId = creatorUserId;
            LastUpdateDate = lastUpdateDate;
            LastUpdatedUserId = lastUpdatedUserId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
