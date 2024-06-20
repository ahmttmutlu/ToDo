using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models
{
    [Table("Users")]
    public class AppUser : BaseModel
    {
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string Photo { get; set; }
        public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();


    }
}
