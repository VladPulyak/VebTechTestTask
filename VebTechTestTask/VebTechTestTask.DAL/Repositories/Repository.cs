using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Exceptions;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly VebTechDbContext _context;
        protected readonly DbSet<TEntity> _set;

        public Repository(VebTechDbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _set.AsNoTracking();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var addedEntity = await _set.AddAsync(entity);
            return addedEntity.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            return _set.Update(entity).Entity;
        }

        public virtual async Task<TEntity> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidArgumentException("Invalid argument");
            }

            var entity = await _set.FindAsync(id);

            if (entity is null)
            {
                throw new ObjectNotFoundException("Object with this id is not found");
            }

            return entity;
        }

        public async Task Delete(string id)
        {
            var entity = await GetById(id);
            _set.Remove(entity);
        }

        public Task Save()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
