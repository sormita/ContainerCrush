using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LoginFunctionality.Models;
using LoginFunctionality.Utility;
using LoginFunctionality.Utility.UtilityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginFunctionality.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // readonly SignInManager<IdentityUser> signInManager;
        //UserManager<User> usrManager;
        private IApiClient apiProxy;

        public AccountController(IApiClient apiClient)
        {
            //this.usrManager = userManager;
            apiProxy = apiClient;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl="/Home")
        {
            return View(new LoginViewModel { ReturnUrl=returnUrl});
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Validate from Web API                 
                var user = await apiProxy.GetAsync<User>("user?email=" + model.Email + "&password=" + model.Password);

                if (user == null)
                    return Unauthorized();

                var claims = new List<Claim> 
                { 
                 new Claim(ClaimTypes.NameIdentifier,user.PersonID.ToString()),
                 new Claim(ClaimTypes.Email, user.PersonEmail),
                 new Claim(ClaimTypes.GivenName,user.PersonName)
                };

                var identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = model.RememberMe }
                    );

                return RedirectToAction("Index", "Home");


                //ModelState.AddModelError(string.Empty, "Invalid login attempt");                 
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
