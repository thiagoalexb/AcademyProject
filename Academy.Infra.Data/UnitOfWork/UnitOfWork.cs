using Academy.Domain.Core.Commands;
using Academy.Domain.Interfaces;
using Academy.Domain.Interfaces.Core;
using Academy.Infra.Data.Context;
using Academy.Infra.Data.Repositories;

namespace Academy.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AcademyContext _context;

        public UnitOfWork(AcademyContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
