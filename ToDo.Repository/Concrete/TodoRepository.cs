using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Data;
using ToDo.Models;
using ToDo.Models.DTOs;
using ToDo.Repository.Abstract;
using ToDo.Repository.Shared.Abstract;
using ToDo.Repository.Shared.Concrete;

namespace ToDo.Repository.Concrete
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
      
        private readonly IRepository<Tag> _tagRepo;
        public TodoRepository(ApplicationDbContext context, IRepository<Tag> tagRepo) : base(context)
        {
           
            _tagRepo = tagRepo;
        }

        public IQueryable<TodoDto> GetAll(int userId)
        {
            return base.GetAll().Where(t => t.AppUserId == userId && t.Status.Name != "Tamamlandı").Select(t => new TodoDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                PriorityColor = t.Priority.Color,
                StatusName = t.Status.Name,
                Tags = t.Tags.ToList(),
            });
        }

        public void RemoveTag(int todoId, int tagId)
        {
            Todo todo =base.GetAll(t=>t.Id==todoId).Include(t=>t.Tags).First();
            todo.Tags.Remove(todo.Tags.FirstOrDefault(t=>t.Id==tagId));
            base.Update(todo);
            
        }
      
        public Todo Add(Todo todo, int userId, int[] tags)
        {
            todo.Tags =_tagRepo.GetAll(t => tags.Contains(t.Id)).ToList();
            todo.AppUserId = userId;
            base.Add(todo);
            return todo;
        }
        public override Todo GetById(int id)
        {
           return base.GetAll(t=>t.Id == id).Select(t => new Todo
           {
               Name = t.Name,
               Id = t.Id,
               Description = t.Description,
               Status = t.Status,
               Tags = t.Tags
           }).First();
        }
        
    }
}
