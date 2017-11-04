using Academy.Domain.Entities;
using System;

namespace Academy.Application.Interfaces
{
    public interface IErrorLogAppService : IDisposable
    {
        void Register(ErrorLog errorLog);
    }
}
