using Academy.Domain.Validations;
using System;

namespace Academy.Domain.Commands
{
    public class UpdateUserCommand : UserCommand
    {
        public UpdateUserCommand(Guid userId, string firstName, string lastName, string email, string password, DateTime dateOfBirth,
                                        DateTime creationDate, Guid? creatorUserId, DateTime? lastUpdateDate, Guid? lastUpdatedUserId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            DateOfBirth = dateOfBirth;
            UserId = userId;
            CreationDate = creationDate;
            CreatorUserId = creatorUserId;
            LastUpdateDate = lastUpdateDate;
            LastUpdatedUserId = lastUpdatedUserId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
