using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MovieRental.MVC.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieRental.MVC.Services
{
    public class AccountService : IAccountService
    {
        private readonly MovieRentalContext _dbContext;
        public AccountService(MovieRentalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Login(User collection, IPasswordHasher<User> passwordHasher, HttpContext httpContext)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == collection.Email);

            if (user != null)
            {
                var checkPassword = passwordHasher.VerifyHashedPassword(collection, user.HashPassword, collection.Password);

                if (checkPassword == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                        IsPersistent = false,
                    };

                    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Register(User collection, HttpContext httpContext, IPasswordHasher<User> passwordHasher, ModelStateDictionary ModelState)
        {
            var CheckEmailDuplicate = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == collection.Email);

            if (CheckEmailDuplicate != null)
            {
                ModelState.AddModelError("InvalidRegister", "Podany email został już wykorzystany");
                return false;
            }

            var passwordHash = passwordHasher.HashPassword(collection, collection.Password);

            collection.HashPassword = passwordHash;

            await _dbContext.AddAsync(collection);
            await _dbContext.SaveChangesAsync();

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, (_dbContext.Users.ToList().Count+1).ToString()),
                        new Claim(ClaimTypes.Email, collection.Email),
                        new Claim(ClaimTypes.Role, collection.Role),
                    };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                IsPersistent = false,
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }
    }
}
