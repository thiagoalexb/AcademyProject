using Academy.Application.Interfaces;
using Academy.Application.ViewModels;
using Academy.Domain.Commands;
using Academy.Domain.Core.Bus;
using Academy.Domain.Interfaces;
using Academy.Domain.Interfaces.Core;
using Academy.Infra.Data.Repositories.EventSourcing;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Academy.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _bus;

        public UserAppService(IMapper mapper,
                                IUserRepository userRepository,
                                IMediatorHandler bus,
                                IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(_userRepository.GetAll());
        }

        public UserViewModel Get(Guid id)
        {
            return _mapper.Map<UserViewModel>(_userRepository.Get(id));
        }

        public void Register(UserViewModel userViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewUserCommand>(userViewModel);
            _bus.SendCommand(registerCommand);
        }

        public void Update(UserViewModel userViewModel)
        {
            var updateCommand = _mapper.Map<UpdateUserCommand>(userViewModel);
            _bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveUserCommand(id);
            _bus.SendCommand(removeCommand);
        }

        public  UserViewModel GetByEmailAndPassword(string email, string password)
        {
            return _mapper.Map<UserViewModel>(_userRepository.GetByEmailAndPassword(email, password));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
