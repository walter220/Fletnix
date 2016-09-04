using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity.Migrations;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
#if !DEBUG
    [RequireHttps]
#endif
    public class AuthController : Controller
    {
        private SignInManager<FletnixUser> _signInManager;
        private UserManager<FletnixUser> _userManager;
        private RoleManager<IdentityRole> _role;
//        private IHistoryRepository _history;

        //        public AuthController(SignInManager<FletnixUser> signInManager, UserManager<FletnixUser> userManager, RoleManager<FletnixUser> roleManager, IHistoryRepository history)
        //        {
        //            _signInManager = signInManager;
        //            _userManager = userManager;
        //            _manager = roleManager;
        //            _history = history;
        //        }

        public AuthController(SignInManager<FletnixUser> signInManager, UserManager<FletnixUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _role = roleManager;
//            _history = history;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Als de persoon is ingelogd, dan wordt deze naar de {Actie}, {Controller} gstuurd
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Als de persoon is ingelogd, dan wordt deze naar de {Actie}, {Controller} gstuurd
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(
                    vm.Username,
                    vm.Password,
                    true, false
                    );
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("Index", "App");
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "App");
                }
                ModelState.AddModelError("", "Username or password incorrect");
                
            }

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }


        public IActionResult Roles()
        {
            return View(_role.Roles.ToList());
        }

        
//        public async Task<ActionResult> Roles(RolesViewModel vm)
//        {
//            if (ModelState.IsValid)
//            {
//                var role = new IdentityRole(vm.Role);
//                var roleresult = await _rmanager.CreateAsync(role);
//                if (!roleresult.Succeeded)
//                {
//                    return View();
//                }
//                return RedirectToAction("Index", "App");
//            }
//            return View(vm);
//        }

    }
}
