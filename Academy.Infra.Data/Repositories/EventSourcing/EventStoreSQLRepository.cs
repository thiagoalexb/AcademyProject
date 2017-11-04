using Academy.Domain.Core.Events;
using Academy.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Academy.Infra.Data.Repositories.EventSourcing
{
    public class EventStoreSQLRepository : IEventStoreRepository
    {
        private readonly AcademyContext _context;

        public EventStoreSQLRepository(AcademyContext context)
        {
            _context = context;
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return _context.StoredEvent.Where(x => x.AggregateId == aggregateId).ToList();
        }

        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvent.Add(theEvent);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
