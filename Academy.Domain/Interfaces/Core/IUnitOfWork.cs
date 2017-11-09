using Academy.Domain.Core.Commands;
using System;

namespace Academy.Domain.Interfaces.Core
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Complete();
        TEntity Repository<TEntity>() where TEntity : class;
    }
}
