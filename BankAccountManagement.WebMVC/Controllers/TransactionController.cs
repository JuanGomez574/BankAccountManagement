using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankAccountManagement.Models;
using BankAccountManagement.Services;

namespace BankAccountManagement.WebMVC.Controllers
{
    public class TransactionController : Controller
    {
        public ActionResult Index()
        {
            TransactionService service = CreateTransactionService();
            var model = service.GetAllTransactions();

            return View(model);
        }
        public ActionResult Create()
        {          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTransactionService();

            if (service.CreateTransaction(model))
            {
                TempData["SaveResult"] = "Transaction successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Transaction could not be created.");

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateTransactionService();
            var detail = service.GetTransactionsById(id);
            var model =
                new TransactionEdit
                {
                    AmountOfTransaction = detail.AmountOfTransaction,
                    TransactionDescription = detail.TransactionDescription,
                    TypeOfTransaction = detail.TypeOfTransaction,
                    SavingsAccountId = detail.SavingsAccountId,
                    CheckingAccountId = detail.CheckingAccountId

                };
            return View(model);
        }
        private TransactionService CreateTransactionService()
        {

            var service = new TransactionService();
            return service;
        }
    }
}
