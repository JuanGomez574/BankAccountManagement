using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankAccountManagement.WebMVC.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            CustomerService service = CreateCustomerService();
            var model = service.GetAllCustomers();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCustomerService();

            if (service.CreateCustomer(model))
            {
                TempData["SaveResult"] = "The customer was added.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Customer could not be created.");

            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateCustomerService();
            var model = svc.GetCustomerById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateCustomerService();
            var detail = service.GetCustomerById(id);
            var model =
                new CustomerEdit
                {
                    CustomerId = detail.CustomerId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    Address = detail.Address,
                    DateOfBirth = detail.DateOfBirth,
                    Email = detail.Email,
                    PhoneNumber = detail.PhoneNumber,
                    SocialSecurityNumber = detail.SocialSecurityNumber

                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CustomerId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCustomerService();

            if (service.UpdateCustomer(model))
            {
                TempData["SaveResult"] = "The customer was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The customer could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCustomerService();
            var model = svc.GetCustomerById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCustomerService();

            service.DeleteCustomer(id);

            TempData["SaveResult"] = "The customer was deleted";

            return RedirectToAction("Index");
        }
        private CustomerService CreateCustomerService()
        {

            var service = new CustomerService();
            return service;
        }
    }
}
