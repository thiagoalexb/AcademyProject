using Academy.Application.Interfaces;
using Academy.Domain.Core.Bus;
using Academy.Domain.Entities;
using Academy.Domain.Interfaces;
using Academy.Domain.Interfaces.Core;
using Academy.Infra.Data.Context;
using Academy.Infra.Data.Repositories;
using Academy.Infra.Data.UnitOfWork;
using System;

namespace Academy.Application.Services
{
    public class ErrorLogAppService : IErrorLogAppService
    {
        private readonly IErrorLogRepository _errorLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ErrorLogAppService(
                                IErrorLogRepository errorLogRepository,
                                IUnitOfWork unitOfWork)
        {
            _errorLogRepository = errorLogRepository;
            _unitOfWork = unitOfWork;
        }

        public void Register(ErrorLog errorLog)
        {
            _errorLogRepository.Add(errorLog);
            _unitOfWork.Complete();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
