using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankAccountManagement.WebMVC.Controllers
{
    public class SavingsAccountController : Controller
    {
        public ActionResult Index()
        {
            SavingsAccountService service = CreateSavingsAccountService();
            var model = service.GetAllSavingsAccounts();

            return View(model);
        }

        public ActionResult Create()
        {
            var svc = CreateSavingsAccountService();

            ViewBag.Customers = svc.GetCustomers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SavingsAccountCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSavingsAccountService();

            if (service.CreateSavingsAccount(model))
            {
                TempData["SaveResult"] = "Savings Account successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Savings Account could not be created.");

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateSavingsAccountService();
            var detail = service.GetSavingsAccountById(id);
            var model =
                new SavingsAccountEdit
                {
                    SavingsBalance = detail.SavingsBalance

                };
            return View(model);
        }
        private SavingsAccountService CreateSavingsAccountService()
        {

            var service = new SavingsAccountService();
            return service;
        }
    }
}
