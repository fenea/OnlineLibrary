using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Domain.Models;
using Persistance;

namespace Presentation.Controllers {     public class AccountController : Controller     {         private readonly ILogger<AccountController> _logger;         private readonly SignInManager<User> _signInManager;         private readonly UserManager<User> _userManager;         private DatabaseContext _db;


         public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager, UserManager<User> userManager, DatabaseContext db)         {             _logger = logger;
            _signInManager = signInManager;             _userManager = userManager;
            _db = db;         }          private void AddErrors(IdentityResult result)         {             foreach (var error in result.Errors)             {                 ModelState.AddModelError("", error.Description);             }         }          public IActionResult Login()         {             if (this.User.Identity.IsAuthenticated)             {                 return RedirectToAction("Index", "Home");
            }              return View();         }


        [HttpPost]         public async Task<IActionResult> Login(LoginViewModel model)         {             if (ModelState.IsValid)             {
                //allow to signin only with username+password
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (result.Succeeded)                 {
                    return RedirectToAction("Index", "Home");
                }              }              ModelState.AddModelError("", "Failed to login ");              return View();         }
          [HttpGet]         public async Task<IActionResult> Logout()         {             await _signInManager.SignOutAsync();             return RedirectToAction("Login");         } 
         public IActionResult Register()         {             return View();         }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new User { UserName = model.Username, Email = model.Email };



                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }


            return View();
        }
       } } 