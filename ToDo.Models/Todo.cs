using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class Todo : BaseModel
    {
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }= DateTime.Now;
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int AppUserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }= new List<Tag>();  
        public int? PriorityId { get; set; }
        public virtual Priority Priority { get; set; }
    }
}
