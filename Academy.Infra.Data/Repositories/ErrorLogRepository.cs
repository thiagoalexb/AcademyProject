using Academy.Domain.Entities;
using Academy.Domain.Interfaces;
using Academy.Infra.Data.Context;
using Academy.Infra.Data.Repositories.Core;
using System;

namespace Academy.Infra.Data.Repositories
{
    public class ErrorLogRepository : Repository<ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(AcademyContext context) : base(context)
        { }
    }
}
