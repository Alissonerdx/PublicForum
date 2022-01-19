using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Auth.Interface
{
    public interface IUser<TKey>
    {
        TKey Id { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        string PhoneNumber { get; set; }
        bool EmailConfirmed { get; set; }
        string NormalizedEmail { get; set; }
        string NormalizedUserName { get; set; }
    }
}
