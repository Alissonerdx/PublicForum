using PublicForum.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Auth.Interface
{
    public interface IUserResolver
    {
        ApplicationUser GetCurrentUser();
        string GetUserId();
        bool IsAuthenticated();
        string GetClaim();
    }
}
