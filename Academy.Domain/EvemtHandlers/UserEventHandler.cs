using Academy.Domain.Events;
using Academy.Domain.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.EvemtHandlers
{
    public class UserEventHandler :
        INotificationHandler<UserRegisteredEvent>,
        INotificationHandler<UserUpdatedEvent>,
        INotificationHandler<UserPasswordUpdatedEvent>,
        INotificationHandler<UserRemovedEvent>
    {

        private readonly IEmailService _emailService;

        public UserEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Handle(UserRegisteredEvent message)
        {
            Task.Run(() => _emailService.SendEmailAsync(message.Email, "Confirmação de senha", $"Para confirmar sua senha <a href='meusite.com/confirmasenha/{message.Email}'>Click aqui</a>"));
        }

        public void Handle(UserUpdatedEvent message)
        {
            
        }

        public void Handle(UserPasswordUpdatedEvent notification)
        {

        }

        public void Handle(UserRemovedEvent message)
        {
            
        }
    }
}
