using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<AppUser> Users { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }

    }
}
