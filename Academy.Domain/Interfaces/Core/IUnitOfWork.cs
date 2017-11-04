using Academy.Domain.Core.Commands;
using System;

namespace Academy.Domain.Interfaces.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
        CommandResponse Commit();
    }
}
