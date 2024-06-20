using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;
using ToDo.Repository.Shared.Abstract;

namespace ToDo.Repository.Abstract
{
    public interface ITodoRepository : IRepository<Todo>
    {
        IEnumerable<Todo> GetAll(bool isAdmin, int userId);
        void DeleteToddoByUserId(int userId);
        void DeleteRemoveTag(int todoId, int  tagId);

        void Add(Todo todo , int[] tags);     
        void Update(Todo todo , int[] tags);
        void TodoComplete(int todoId);

    }
}
