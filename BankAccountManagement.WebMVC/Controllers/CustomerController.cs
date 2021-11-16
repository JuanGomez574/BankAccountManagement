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
        // GET: Customer
        public ActionResult Index()
        {
            CustomerService service = CreateCustomerService();
            var model = service.GetAllCustomers();

            return View(model);
        }

        //GET: CustomerCreate
        public ActionResult Create()
        {
            return View();
        }

        //POST: Customer
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


        //GET: CustomerUpdate
        [HttpGet]

        public ActionResult Edit(int id)
        {
            var service = CreateCustomerService();
            var detail = service.GetCustomerById(id);
            var model =
                new CustomerEdit
                {
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



        //Helper method(s)
        private CustomerService CreateCustomerService()
        {

            var service = new CustomerService();
            return service;
        }
    }
}
