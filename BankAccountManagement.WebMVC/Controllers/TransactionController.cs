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
        public ActionResult Details(int id)
        {
            var svc = CreateTransactionService();
            var model = svc.GetTransactionsById(id);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransactionEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TransactionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTransactionService();

            if (service.UpdateTransaction(model))
            {
                TempData["SaveResult"] = "The transaction was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The transaction could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateTransactionService();
            var model = svc.GetTransactionsById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTransactionService();

            service.DeleteTransaction(id);

            TempData["SaveResult"] = "The transaction was deleted";

            return RedirectToAction("Index");
        }
        private TransactionService CreateTransactionService()
        {

            var service = new TransactionService();
            return service;
        }
    }
}
