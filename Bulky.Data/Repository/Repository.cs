using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyWeb.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //_db.Categoris = dbSet
            
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> fileter, string? includeroerties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(fileter);
            if (!string.IsNullOrEmpty(includeroerties))
            {
                foreach (var includeProp in includeroerties.
                    Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) // include other models properties 
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        //category , civertype
        public IEnumerable<T> GetAll(string? includeroerties = null)
        {

            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeroerties))
            {
                foreach (var includeProp in includeroerties.
                    Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries))// include other models properties 
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
