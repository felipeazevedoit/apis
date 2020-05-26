using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TServices.Comum.Contracts.Repositories
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly DbContext Db;
        protected DbSet<TEntity> DbSet;

        public RepositoryBase(DbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity model)
        {
            try
            {
                //Db.Entry(model).State = EntityState.Added;
                await DbSet.AddAsync(model);
                Db.SaveChanges();

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddRange(params TEntity[] models)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            try
            {
                IQueryable<TEntity> dbQuery = DbSet;

                //Apply eager loading
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                var list = dbQuery
                         .AsNoTracking()
                         .ToListAsync<TEntity>();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<TEntity>> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = DbSet.AsQueryable();

            var query = Db.Set<TEntity>().AsQueryable();

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            var list = dbQuery
                        .AsNoTracking()
                        .Where(where)
                        .ToList<TEntity>();

            return list;
        }

        public Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = DbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            var item = dbQuery
                 .AsNoTracking() //Don't track any changes for the selected item
                 .FirstOrDefaultAsync(where); //Apply where clause

            return item;
        }
        
        public async Task Remove(TEntity model)
        {
            DbSet.Remove(model);
            Db.SaveChanges();
        }

        public async Task RemoveRange(params TEntity[] models)
        {
            throw new NotImplementedException();
        }

        public async Task Update(TEntity model)
        {
            DbSet.Update(model);
            Db.SaveChanges();
        }

        public Task UpdateRange(params TEntity[] models)
        {
            throw new NotImplementedException();
        }
    }
}
