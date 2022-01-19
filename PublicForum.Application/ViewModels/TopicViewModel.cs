using PublicForum.Auth.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Application.ViewModels
{
    public class TopicViewModel : BaseViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }
        public bool IsDeleted { get; set; }
    }
}
