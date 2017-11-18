using Academy.Domain.Commands;
using Academy.Domain.Core.Bus;
using Academy.Domain.Core.Notifications;
using Academy.Domain.Entities;
using Academy.Domain.Events;
using Academy.Domain.Interfaces;
using Academy.Domain.Interfaces.Core;
using Academy.Domain.Services.Interfaces;
using Academy.Domain.Services.Utils;
using MediatR;
using System;
using System.Linq;

namespace Academy.Domain.CommandHandlers
{
    public class UserCommandHandler : CommandHandler,
        INotificationHandler<RegisterNewUserCommand>,
        INotificationHandler<UpdateUserCommand>,
        INotificationHandler<UpdateUserPasswordCommand>,
        INotificationHandler<RemoveUserCommand>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(  IUnitOfWork uow,
                                    IMediatorHandler bus, 
                                    IEmailService emailService,
                                    INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _bus = bus;
            _uow = uow;
            _userRepository = _uow.Repository<IUserRepository>();
        }

        public void Handle(RegisterNewUserCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var user = new User(Guid.NewGuid(), message.FirstName, message.LastName,
                                    message.Email, message.DateOfBirth, 
                                    message.CreationDate, message.CreatorUserId, message.LastUpdateDate, message.LastUpdatedUserId); 

            if (_userRepository.Find(x => x.Email == user.Email).Count() > 0)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Este e-mail já está sendo usado"));
                return;
            }

            _userRepository.Add(user);

            if (Commit())
            {
                _bus.RaiseEvent(new UserRegisteredEvent(user.UserId, user.FirstName, user.LastName,
                                    user.Email, user.Password, user.DateOfBirth,
                                    user.CreationDate, user.CreatorUserId, user.LastUpdateDate, user.LastUpdatedUserId));
            }
        }

        public void Handle(UpdateUserCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var existingUser = _userRepository.Get(message.UserId);

            if (existingUser != null)
            {
                if (existingUser.Email != message.Email && _userRepository.Find(x => x.Email == message.Email) != null)
                {
                    _bus.RaiseEvent(new DomainNotification(message.MessageType, "Este e-mail já está sendo usado"));
                    return;
                }

                existingUser.FirstName = message.FirstName;
                existingUser.LastName = message.LastName;
                existingUser.Email = message.Email;
                existingUser.DateOfBirth = message.DateOfBirth;
                existingUser.IsVerified = message.IsVerified;
                existingUser.CreationDate = message.CreationDate;
                existingUser.CreatorUserId = message.CreatorUserId;
                existingUser.LastUpdateDate = message.LastUpdateDate;
                existingUser.LastUpdatedUserId = message.LastUpdatedUserId;
            }

            if (Commit())
            {
                _bus.RaiseEvent(new UserUpdatedEvent(existingUser.UserId, existingUser.FirstName, existingUser.LastName,
                                    existingUser.Email, existingUser.Password, existingUser.DateOfBirth, existingUser.IsVerified,
                                    existingUser.CreationDate, existingUser.CreatorUserId, existingUser.LastUpdateDate, existingUser.LastUpdatedUserId));
            }
        }

        public void Handle(UpdateUserPasswordCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var existingUser = _userRepository.Get(message.UserId);

            if (existingUser != null && !existingUser.IsVerified)
            {
                existingUser.Password = Utils.EncryptPassword(message.Password);
                existingUser.IsVerified = true;
                existingUser.CreationDate = message.CreationDate;
                existingUser.CreatorUserId = message.UserId;
                existingUser.LastUpdateDate = message.LastUpdateDate;
                existingUser.LastUpdatedUserId = message.UserId;
            }

            if (Commit())
            {
                _bus.RaiseEvent(new UserPasswordUpdatedEvent(existingUser.UserId, existingUser.Password, existingUser.IsVerified,
                                    existingUser.CreationDate, existingUser.CreatorUserId, existingUser.LastUpdateDate, existingUser.LastUpdatedUserId));
            }
        }

        public void Handle(RemoveUserCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            _userRepository.Remove(_userRepository.Get(message.UserId));

            if (Commit())
            {
                _bus.RaiseEvent(new UserRemovedEvent(message.UserId));
            }
        }
    }
}
