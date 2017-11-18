using Academy.Domain.Core.Commands;
using Academy.Domain.Interfaces.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;

namespace Academy.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private Hashtable repositories;

        public UnitOfWork(DbContext context) => 
            _context = context;

        public TEntity Repository<TEntity>() where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Hashtable();
            }

            var typeName = typeof(TEntity).Name;

            if (repositories.ContainsKey(typeName))
            {
                return (TEntity)repositories[typeName];
            }

            var type = typeof(TEntity);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            foreach (var item in types)
            {
                repositories.Add(type, Activator.CreateInstance(item, _context));
            }

            return (TEntity)repositories[type];
        }

        public CommandResponse Complete()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose() => 
            _context.Dispose();
    }
}
