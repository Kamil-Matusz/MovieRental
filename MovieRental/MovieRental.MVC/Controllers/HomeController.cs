using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRental.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using MovieRental.MVC.Models.Contact;
using MovieRental.MVC.Services;

namespace MovieRental.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;

        public HomeController(ILogger<HomeController> logger, EmailAddress _fromAddress, IEmailService _emailService)
        {
            _logger = logger;
            FromAndToEmailAddress = _fromAddress;
            EmailService = _emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                EmailMessage msgToSend = new EmailMessage
                {
                    FromAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    ToAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    Content = $"Here is your message: Name: {model.Name}, " +$"Email: {model.Email}, Message: {model.Message}",
                    Subject = "Contact Form - BasicContactForm App"
                };

                EmailService.Send(msgToSend);
                return RedirectToAction("Contact");
            }
            else
            {
                return Index();
            }
        }
    }
}
