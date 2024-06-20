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
    public interface IUserRepository : IRepository<AppUser>
    {
        IQueryable<UserProfileDto> GetAllWithTodoCount();

    }
}
