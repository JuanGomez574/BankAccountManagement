using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankAccountManagement.WebMVC.Controllers
{
    public class CheckingAccountController : Controller
    {
        // GET: CheckingAccount
        public ActionResult Index()
        {
            CheckingAccountService service = CreateCheckingAccountService();
            var model = service.GetAllCheckingAccounts();

            return View(model);
        }

        //GET: TransactionCreate
        public ActionResult Create()
        {
            return View();
        }

        //POST: Transaction
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


        //GET: CheckingAccountUpdate
        [HttpGet]

        public ActionResult Edit(int id)
        {
            var service = CreateCheckingAccountService();
            var detail = service.GetCheckingAccountById(id);
            var model =
                new CheckingAccountEdit
                {
                    CheckingBalance = detail.CheckingBalance,

                };
            return View(model);
        }



        //Helper method(s)
        private CheckingAccountService CreateCheckingAccountService()
        {

            var service = new CheckingAccountService();
            return service;
        }
    }
}
