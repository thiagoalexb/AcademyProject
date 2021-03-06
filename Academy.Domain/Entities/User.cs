﻿using Academy.Domain.Entities.Core;
using Academy.Domain.Interfaces.Core;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Academy.Domain.Entities
{
    public class User : Entity, IUser
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsVerified { get; set; } = false;
        public string Name { get => $"{FirstName} {LastName}"; }

        public override string ToString() => $"Id: {UserId} - {FirstName}";

        public User() { }

        public User(Guid userId, string firstName, string lastName, string email, DateTime dateOfBirth,
                    DateTime creationDate, Guid? creatorUserId, DateTime? lastUpdateDate, Guid? lastUpdatedUserId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            CreationDate = creationDate;
            CreatorUserId = creatorUserId;
            LastUpdateDate = lastUpdateDate;
            LastUpdatedUserId = lastUpdatedUserId;
        }
    }
}
