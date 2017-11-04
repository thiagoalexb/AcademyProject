using Academy.Domain.Entities;
using Academy.Domain.Interfaces;
using Academy.Infra.Data.Context;
using Academy.Infra.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Academy.Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AcademyContext context) : base(context)
        { }

        public User GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Email == email);
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
