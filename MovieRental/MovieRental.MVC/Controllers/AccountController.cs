using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieRental.MVC.Models;
using MovieRental.MVC.Services;
using System.Threading.Tasks;

namespace MovieRental.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private IPasswordHasher<User> _passwordHasher;

        public AccountController(IAccountService accountService, IPasswordHasher<User> passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }
        public ActionResult Login()
        {
            return View();
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: Logout
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("/Home/Index");
        }

        // POST: AccountController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(User collection)
        {
            var loginService = await _accountService.Login(collection, _passwordHasher, HttpContext);

            if (loginService)
            {
                return LocalRedirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("InvalidLogin", "Podane dane są nieprawidłowe");
                return View();
            }
        }

        // POST: AccountController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(User collection)
        {
            ModelState.ClearValidationState(nameof(collection));
            if (!TryValidateModel(collection, nameof(collection)))
            {
                var registerService = await _accountService.Register(collection, HttpContext, _passwordHasher, ModelState);

                if (registerService)
                {
                    return LocalRedirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError("InvalidRegister", "Podany email już istnieje!");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("InvalidRegister", "Podane dane są nieprawidłowe!");
                return View();
            }
        }
    }
}
