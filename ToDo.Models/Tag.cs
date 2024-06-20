using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class Tag : BaseModel
    {
        public virtual ICollection<Todo> Todos { get; set; }= new List<Todo>();
    }

}
