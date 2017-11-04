using Academy.Domain.Entities;
using Academy.Domain.Interfaces.Core;

namespace Academy.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
        User GetByEmailAndPassword(string email, string password);
    }
}
