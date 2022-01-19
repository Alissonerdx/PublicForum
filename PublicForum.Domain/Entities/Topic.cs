using PublicForum.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Domain.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string OwnerId { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUser Owner { get; set; }
    }
}
