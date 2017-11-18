using Academy.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace Academy.Application.Interfaces
{
    public interface IUserAppService : IDisposable
    {
        IEnumerable<UserViewModel> GetAll();
        UserViewModel Get(Guid id);
        UserViewModel GetByEmailAndPassword(string email, string password);
        UserUpdatePasswordViewModel GetByEmail(string email);

        void Register(UserViewModel userViewModel);
        void Update(UserViewModel userViewModel);
        void UpdatePassword(UserUpdatePasswordViewModel userViewModel);
        void Remove(Guid id);
        
    }
}
