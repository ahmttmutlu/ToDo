using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models.DTOs
{
    public class TodoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PriorityColor { get; set; }
        public string StatusName { get; set; }
        public List<Tag> Tags { get; set; }

    }
}
