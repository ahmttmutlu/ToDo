using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Repository.Shared.Abstract
{
    public interface IRepository<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        T Add(T entity);
        T Update(T entity);
        void Delete(int id);
        T GetById(int id);
        T GetFirstOrDefault(Expression<Func<T, bool>> expression);
        void Save();
    }
}
