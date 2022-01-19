using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PublicForum.Application.ViewModels;
using PublicForum.Auth.Model;

namespace PublicForum.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Account/Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerUser)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Fill in all mandatory fields" });

            var user = new ApplicationUser
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
                return Json(new { Success = true, Message = "User registered succefully" });

            return Json(new { Success = false, Message = String.Join("<br/>", result.Errors.Select(e => e.Description)) });
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Message = "Fill in all mandatory fields" });

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
                return Json(new { Success = false, Message = "Username or Password is invalid" });

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (!result.Succeeded)
                return Json(new { Success = false, Message = "Username or Password is invalid" });

            return Json(new { Success = true, Message = "User logged", Redirect = "/Topic/Index" });
        }
    }
}
