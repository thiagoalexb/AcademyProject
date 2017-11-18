using Academy.Domain.Validations;
using System;

namespace Academy.Domain.Commands
{
    public class UpdateUserPasswordCommand : UserCommand
    {
        public UpdateUserPasswordCommand(Guid userId, string password,
                                        DateTime creationDate, Guid? creatorUserId, DateTime? lastUpdateDate, Guid? lastUpdatedUserId)
        {
            Password = password;
            UserId = userId;
            CreationDate = creationDate;
            CreatorUserId = creatorUserId;
            LastUpdateDate = lastUpdateDate;
            LastUpdatedUserId = lastUpdatedUserId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserPasswordCommandVailidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
