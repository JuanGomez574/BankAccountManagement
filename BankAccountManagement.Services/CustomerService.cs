using BankAccountManagement.Data;
using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    public class CustomerService
    {
        public bool CreateCustomer(CustomerCreate model)
        {
            var entity =
                new Customer()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    SocialSecurityNumber = model.SocialSecurityNumber
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CustomerListItem> GetAllCustomers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Customers
                    .Where(e => e.CustomerId == e.CustomerId)
                    .Select(
                        e =>
                        new CustomerListItem
                        {
                            CustomerId = e.CustomerId,
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            
                            Address = e.Address,
                            DateOfBirth = e.DateOfBirth,
                            Email = e.Email,
                            PhoneNumber = e.PhoneNumber,
                            SocialSecurityNumber = e.SocialSecurityNumber
                        });
                return query.ToArray();
            }
        }
        public CustomerDetail GetCustomerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity= 
                    ctx
                    .Customers
                    .Single(e => e.CustomerId == id);

               
                
                    return
                        new CustomerDetail
                        {
                            CustomerId = entity.CustomerId,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            FullName = entity.FullName,
                            Address = entity.Address,
                            DateOfBirth = entity.DateOfBirth,
                            Email = entity.Email,
                            PhoneNumber = entity.PhoneNumber,
                            SocialSecurityNumber = entity.SocialSecurityNumber
                        };
                

            };

        }
           


        public bool UpdateCustomer(CustomerEdit model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Customers
                    .Single(e => e.CustomerId == id);

                entity.FirstName = model.FirstName;
                            entity.LastName = model.LastName;
                            entity.Address = model.Address;
                            entity.DateOfBirth = model.DateOfBirth;
                            entity.Email = model.Email;
                           entity.PhoneNumber = model.PhoneNumber;
                           entity.SocialSecurityNumber = model.SocialSecurityNumber;

                return ctx.SaveChanges() == 1;
            }
        }

        
    }
}

