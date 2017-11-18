using Academy.Domain.Entities;
using Academy.Domain.Interfaces;
using Academy.Infra.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Academy.Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        { }

        public User GetByEmail(string email) =>
            DbSet.FirstOrDefault(x => x.Email == email);

        public User GetByEmailAndPassword(string email, string password) =>
            DbSet.FirstOrDefault(x => x.Email == email && x.Password == password);
    }
}
