using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TServices.Comum.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity model);
        Task AddRange(params TEntity[] models);
        Task Update(TEntity model);
        Task UpdateRange(params TEntity[] models);

        Task Remove(TEntity model);
        Task RemoveRange(params TEntity[] models);

        Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<IList<TEntity>> GetList(Func<TEntity, bool> where,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        void Dispose();
    }
}