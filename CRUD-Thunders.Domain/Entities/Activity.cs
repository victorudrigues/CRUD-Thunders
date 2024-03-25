using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Thunders.Domain.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
