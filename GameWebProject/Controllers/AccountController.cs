

/*
 * <summary>
 * this controller class is responsible for creating a user and signing in and out a user
 * </summary>
 */

using GameWebProject.Models.UserModel;
using GameWebProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameWebProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        public async Task<IActionResult> isEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"email {email} is already in use");
            }
        }

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        public async Task<IActionResult> isUserNameValid(string userName)
        {
            var user = await userManager.FindByEmailAsync(userName);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"user name {userName} is taken");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {FirstName = model.FirstName, LastName = model.LastName,
                    UserName = model.UserName, Email = model.Email, ZipCode = model.ZipCode };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, 
                    model.Password, model.RememberMe, false);
                

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
              

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }
    }
}
