using Academy.Domain.Core.Events;
using Academy.Domain.Entities.Core;
using System;

namespace Academy.Domain.Events
{
    public class UserUpdatedEvent : EntityEvent
    {
        public UserUpdatedEvent(Guid id, string firstName, string lastName, string email, string password, DateTime birthDate,
                                DateTime creationDate, Guid? creatorUserId, DateTime? lastUpdateDate, Guid? lastUpdatedUserId)
        {
            UserId = id;
            FirstName = firstName;
            FirstName = lastName;
            LastName = email;
            Password = password;
            DateOfBirth = birthDate;
            AggregateId = id;
            CreationDate = creationDate;
            CreatorUserId = creatorUserId;
            LastUpdateDate = lastUpdateDate;
            LastUpdatedUserId = lastUpdatedUserId;
        }
        public Guid UserId { get; set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public DateTime DateOfBirth { get; private set; }
    }
}
