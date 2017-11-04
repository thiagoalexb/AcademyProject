using Academy.Application.Interfaces;
using Academy.Application.ViewModels;
using Academy.Domain.Commands;
using Academy.Domain.Core.Bus;
using Academy.Domain.Interfaces;
using Academy.Infra.Data.Repositories.EventSourcing;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Academy.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public UserAppService(IMapper mapper,
                                IUserRepository userRepository,
                                IMediatorHandler bus,
                                IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(_userRepository.GetAll()); ;
        }

        public UserViewModel Get(Guid id)
        {
            return _mapper.Map<UserViewModel>(_userRepository.Get(id));
        }

        public void Register(UserViewModel userViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewUserCommand>(userViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(UserViewModel userViewModel)
        {
            var updateCommand = _mapper.Map<UpdateUserCommand>(userViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveUserCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public UserViewModel GetByEmailAndPassword(string email, string password)
        {
            return _mapper.Map<UserViewModel>(_userRepository.GetByEmailAndPassword(email, password));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
