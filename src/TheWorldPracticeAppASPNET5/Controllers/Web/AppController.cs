using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using TheWorldPracticeAppASPNET5.Models;
using TheWorldPracticeAppASPNET5.ViewModels;
using Microsoft.AspNet.Authorization;

namespace TheWorldPracticeAppASPNET5.Controllers.Web
{
    public class AppController:Controller
    {
        private ImailService _mailService;
        private IWorldRepository _repository;
        public AppController(ImailService mailService,IWorldRepository repository)
        {
            try
            {
                _mailService = mailService;
                _repository = repository;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult Index()
        {
            var trips = _repository.GetAllTrips();
            return View(trips);
        }
        [Authorize]
        public IActionResult Trips()
        {
            var trips = _repository.GetAllTrips();
            return View(trips);
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
