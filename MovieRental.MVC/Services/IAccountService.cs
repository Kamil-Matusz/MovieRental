using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MovieRental.MVC.Models;
using System.Threading.Tasks;

namespace MovieRental.MVC.Services
{
    public interface IAccountService
    {
        Task<bool> Login(User collection, IPasswordHasher<User> passwordHasher, HttpContext httpContext);
        Task<bool> Register(User collection, HttpContext httpContext, IPasswordHasher<User> passwordHasher, ModelStateDictionary ModelState);
    }
}
