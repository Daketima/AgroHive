using FarmMartBLL.Core;
using FarmMartDAL.Model;
using FarmMartUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Controllers
{
    public class MessagingController : Controller
    {
        readonly IRepositoryService<Messaging> MessagingService;


        public MessagingController(IRepositoryService<Messaging> messagingService) => MessagingService = messagingService;
        // GET: Messaging

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: Messaging/Details/5
        public ActionResult SendMessage(int? messageRecipientId)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Register", "Account", new { area = "" });
            }

            string user = User.Identity.GetUserId();

            //var personToMessage = PerosnService.GetById(messageRecipientId.Value);
            //Person loggedInPersonIsSender = PerosnService.Get().Where(x => x.ApplicationUserId.Equals(user)).FirstOrDefault();
            //MessagingViewModel model = null;

            model = new MessagingViewModel
            {
                ToPersonId = (int)personToMessage ?.Id,
                FromPersonId = (int)loggedInPersonIsSender ?.Id,
                ToPerson = personToMessage ?.CompanyName,
                FromPerson = loggedInPersonIsSender ?.CompanyName
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SendMessage(MessagingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newMessage = new Messaging
                {
                    FromPersonId = model.FromPersonId,
                    ToPerson = model.ToPersonId,
                    MessageToPost = model.MessageToPost,
                    Subject = model.Subject,
                    DateCreated = DateTime.Now
                    
                };
                MessagingService.Create(newMessage);
            }
            return View();
        }

        // GET: Messaging/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messaging/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Messaging/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Messaging/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Messaging/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Messaging/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
