using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using TheWorldPracticeAppASPNET5.Models;
using TheWorldPracticeAppASPNET5.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorldPracticeAppASPNET5.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signInmanager;

        public AuthController(SignInManager<WorldUser> signInmanager)
        {
            _signInmanager = signInmanager;
        }

        // GET: /<controller>/
        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");

            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInmanager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {

                        return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }



                  

                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");


                }
      
            }



            return View();
        }

        public async Task<ActionResult> Logout()
        {

            if (User.Identity.IsAuthenticated)
            {

                await _signInmanager.SignOutAsync();

            }

            return RedirectToAction("Index", "App");

        }
    }
}
