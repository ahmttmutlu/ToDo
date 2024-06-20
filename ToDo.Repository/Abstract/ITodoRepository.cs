using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;
using ToDo.Models.DTOs;
using ToDo.Repository.Shared.Abstract;

namespace ToDo.Repository.Abstract
{
    public interface ITodoRepository : IRepository<Todo>
    {
        IQueryable<TodoDto> GetAll(int userId);
        void RemoveTag(int todoId, int tagId);
         Todo Add(Todo todo, int userId, int[] tags);

    }
}
