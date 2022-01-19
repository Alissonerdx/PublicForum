using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PublicForum.Auth.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Auth.Model
{
    public class UserResolver : IUserResolver
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ClaimsPrincipal _user;

        public UserResolver(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager)
        {
            if (accessor.HttpContext != null)
            {
                _user = accessor.HttpContext.User;
            }
            _userManager = userManager;
        }

        public string GetClaim()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetCurrentUser()
        {
            if (!_user.Identity.IsAuthenticated)
                return null;

            var userId = _userManager.GetUserId(_user);
            return _userManager.FindByIdAsync(userId).Result;
        }

        public string GetUserId()
        {
            return !_user.Identity.IsAuthenticated ? null : _userManager.GetUserId(_user);
        }

        public bool IsAuthenticated()
        {
            return _user.Identity.IsAuthenticated;
        }
    }
}
