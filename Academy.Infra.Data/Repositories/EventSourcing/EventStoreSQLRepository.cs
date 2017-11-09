using Academy.Domain.Core.Events;
using Academy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Academy.Infra.Data.Repositories.EventSourcing
{
    public class EventStoreSQLRepository : IEventStoreRepository
    {
        private readonly DbContext _context;

        public EventStoreSQLRepository(DbContext context)
        {
            _context = context;
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return _context.Set<StoredEvent>().Where(x => x.AggregateId == aggregateId).ToList();
        }

        public void Store(StoredEvent theEvent)
        {
            _context.Set<StoredEvent>().Add(theEvent);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
