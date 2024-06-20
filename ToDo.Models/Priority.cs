using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class Priority : BaseModel
    {
        public string Color { get; set; }
        public virtual ICollection<Todo> Todos { get; set; }
    }
}
