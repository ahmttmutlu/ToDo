using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Data;
using ToDo.Models;
using ToDo.Repository.Shared.Abstract;

namespace ToDo.Repository.Shared.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            Save();
            return entity;
        }

        //public T Delete(T entity)
        //{
        //    _dbSet.Remove(entity);
        //    Save(); 
        //    return entity;
        //}

        public T DeleteById(int id)
        {
            T entity = _dbSet.Find(id);
            if(entity!= null)
            {
                _dbSet.Remove(entity);
                Save();

            }
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return GetAll().Where(filter);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            return _dbSet.FirstOrDefault(filter);
        }

        public void Save()
        {
          _context.SaveChanges();
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            Save();
            return entity;
        }
    }
}