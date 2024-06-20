using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Data;
using ToDo.Models;
using ToDo.Models.DTOs;
using ToDo.Repository.Abstract;
using ToDo.Repository.Shared.Concrete;

namespace ToDo.Repository.Concrete
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public IQueryable<UserProfileDto> GetAllWithTodoCount()
        {
            return base.GetAll().Select(user => new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                IsAdmin = user.IsAdmin,
                Photo = user.Photo,
                TodoCount = user.Todos.Count
            });
        }
    }
}
