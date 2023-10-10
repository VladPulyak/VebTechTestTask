using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> Add(TEntity entity);

        TEntity Update(TEntity entity);

        Task<TEntity> GetById(string id);

        Task Delete(string id);

        Task Save();
    }
}
