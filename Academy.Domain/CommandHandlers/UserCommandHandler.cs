using Academy.Domain.Commands;
using Academy.Domain.Core.Bus;
using Academy.Domain.Core.Notifications;
using Academy.Domain.Entities;
using Academy.Domain.Events;
using Academy.Domain.Interfaces;
using Academy.Domain.Interfaces.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy.Domain.CommandHandlers
{
    public class UserCommandHandler : CommandHandler,
        INotificationHandler<RegisterNewUserCommand>,
        INotificationHandler<UpdateUserCommand>,
        INotificationHandler<RemoveUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _bus;

        public UserCommandHandler(IUserRepository userRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _userRepository = userRepository;
            _bus = bus;
        }

        public void Handle(RegisterNewUserCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var user = new User(Guid.NewGuid(), message.FirstName, message.LastName,
                                    message.Email, message.Password, message.DateOfBirth);

            if (_userRepository.GetByEmail(user.Email) != null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
                return;
            }

            _userRepository.Add(user);

            if (Commit())
            {
                _bus.RaiseEvent(new UserRegisteredEvent(user.UserId, user.FirstName, user.LastName,
                                    user.Email, user.Password, user.DateOfBirth));
            }
        }

        public void Handle(UpdateUserCommand message)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }

            var user = new User(message.UserId, message.FirstName, message.LastName,
                                    message.Email, message.Password, message.DateOfBirth);

            var existingCustomer = _userRepository.GetByEmail(user.Email);

            if (existingCustomer != null && existingCustomer.UserId != user.UserId)
            {
                if (!existingCustomer.Equals(user))
                {
                    _bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
                    return;
                }
            }

            _userRepository.Update(user);

            if (Commit())
            {
                _bus.RaiseEvent(new UserUpdatedEvent(user.UserId, user.FirstName, user.LastName,
                                    user.Email, user.Password, user.DateOfBirth));
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
