using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{

    /// <summary>
    /// Tem metodos genericos aqui kkkkkk salva essa merda so passando o tipo   - Daora :)
    /// </summary>
    /// <typeparam name="T">Tipo da class </typeparam>
    public interface IBase<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        Object Add(params T[] items);
        void Update(params T[] items);
        void Remove(params T[] items);
    }

    /// <summary>
    /// Tem metodos genericos aqui kkkkkk salva essa merda so passando o tipo 
    /// </summary>
    /// <typeparam name="T">Tipo da class</typeparam>
    public class Base<T> : IBase<T> where T : class
    {
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            var context = new wpContext();

            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .ToList<T>();

            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list = new List<T>();
            var context = new wpContext();
            IQueryable<T> dbQuery = context.Set<T>().AsQueryable();

            var query = context.Set<T>().AsQueryable();

            //Apply eager loading

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>();


            return list;
        }

        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            var context = new wpContext();
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause

            return item;
        }

        public virtual object Add(params T[] items)
        {
            var context = new wpContext();
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Added;
            }
           return context.SaveChanges();

        }
        public virtual void Update(params T[] items)
        {
            var context = new wpContext();
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Modified;
            }
            context.SaveChanges();

        }
        public virtual void Remove(params T[] items)
        {
            var context = new wpContext();
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Deleted;
            }
            context.SaveChanges();

        }
    }

}
