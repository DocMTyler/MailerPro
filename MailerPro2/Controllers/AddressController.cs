using MailerPro2.Models;
using MailerPro2.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailerPro2.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AddressService(userID);
            var model = service.ListAddress();

            return View(model);
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressAdd model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateAddressService();

            if (service.AddAddress(model))
            {
                TempData["Save Result"] = "Address has been added";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Address could not be added.");

            return View(model);
        }

        //GET: Read
        public ActionResult Details(int id)
        {
            var service = CreateAddressService();
            var model = service.IndAddress(id);

            return View(model);
        }

        //GET: Update
        public ActionResult Edit(int id)
        {
            var service = CreateAddressService();
            var detail = service.IndAddress(id);
            var model = new AddressUpdate
            {
                FullName = detail.FullName,
                StreetAddress = detail.StreetAddress,
                City = detail.City,
                State = detail.State,
                ZipCode = detail.ZipCode
            };

            return View(model);
        }

        //POST: Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AddressUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ID != id)
            {
                ModelState.AddModelError("", "Address does not match");
                return View(model);
            }

            var service = CreateAddressService();

            if (service.UpdateAddress(model))
            {
                TempData["SaveResult"] = "Address has been updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Law could not be updated");
            return View(model);
        }

        //GET: Delete
        public ActionResult Delete(int id)
        {
            var service = CreateAddressService();
            var model = service.IndAddress(id);

            return View(model);
        }

        //POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAddress(int id)
        {
            var service = CreateAddressService();

            if (service.DeleteAddress(id))
            {
                TempData["Save Result"] = "Address deleted";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Address could not be deleted");
            return View();
        }

        private AddressService CreateAddressService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AddressService(userID);
            return service;
        }
    }
}