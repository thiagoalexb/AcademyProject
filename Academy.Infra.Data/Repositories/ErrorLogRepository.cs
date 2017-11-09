using Academy.Domain.Entities;
using Academy.Domain.Interfaces;
using Academy.Infra.Data.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace Academy.Infra.Data.Repositories
{
    public class ErrorLogRepository : Repository<ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(DbContext context) : base(context)
        { }
    }
}
