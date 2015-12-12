using Microsoft.AspNet.Mvc;
using System;
using TheWorldPracticeAppASPNET5.ViewModels;

namespace TheWorldPracticeAppASPNET5.Controllers.Web
{
    public class AppController:Controller
    {
        private ImailService _mailService;

        public AppController(ImailService mailService)
        {
            try
            {
                _mailService = mailService;
            }
            catch (Exception)
            {

                throw;
            }
           



        }


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult About()
        {

            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {

                var email = Startup.Configuration["AppSettings:SiteEmailAdress"];
                if (string.IsNullOrWhiteSpace(email)) {
                    ModelState.AddModelError("", "Could not send Email, Server Configuration problem");
                }
                if (_mailService.SendMail(email, email, $"Contact Page from {model.Name} ({model.Email})", model.Message)) {
                    ModelState.Clear();
                    ViewBag.Message = "Mail Sent, Thanks!";


                };

            }

           
            return View();
        }


    }




}
