using BankAccountManagement.Models;
using BankAccountManagement.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankAccountManagement.WebMVC.Controllers
{
    public class CheckingAccountController : Controller
    {
        public ActionResult Index()
        {
            CheckingAccountService service = CreateCheckingAccountService();
            var model = service.GetAllCheckingAccounts();

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CheckingAccountCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCheckingAccountService();

            if (service.CreateCheckingAccount(model))
            {
                TempData["SaveResult"] = "Checking Account successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Checking Account could not be created.");

            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateCheckingAccountService();
            var model = svc.GetCheckingAccountById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateCheckingAccountService();
            var detail = service.GetCheckingAccountById(id);
            var model =
                new CheckingAccountEdit
                {
                    CheckingAccountId = detail.CheckingAccountId,
                    CheckingBalance = detail.CheckingBalance,

                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CheckingAccountEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CheckingAccountId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCheckingAccountService();

            if (service.UpdateCheckingAccount(model))
            {
                TempData["SaveResult"] = "The checking account was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The checking account could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCheckingAccountService();
            var model = svc.GetCheckingAccountById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCheckingAccountService();

            service.DeleteCheckingAccount(id);

            TempData["SaveResult"] = "The checking account was deleted";

            return RedirectToAction("Index");
        }
        private CheckingAccountService CreateCheckingAccountService()
        {
            var employeeId = Guid.Parse(User.Identity.GetUserId());
            var service = new CheckingAccountService(employeeId);
            return service;
        }
    }
}
