using Academy.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace Academy.Application.Interfaces
{
    public interface IUserAppService : IDisposable
    {
        void Register(UserViewModel userViewModel);
        IEnumerable<UserViewModel> GetAll();
        UserViewModel Get(Guid id);
        void Update(UserViewModel customerViewModel);
        void Remove(Guid id);
        UserViewModel GetByEmailAndPassword(string email, string password);
    }
}
