using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        //Access the database
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult New()
        {
            var memberShipTypes = _context.MemberShipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MemberShipTypes = memberShipTypes
            };

            return View("CustomerForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            //Validate the form
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MemberShipTypes = _context.MemberShipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(x => x.Id == customer.Id);

//                TryUpdateModel(customerInDb);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        
        // GET: Customers
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MemberShipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customers = _context.Customers.Include(c => c.MemberShipType).SingleOrDefault(c => c.Id == id);

            if (customers == null)
                return HttpNotFound();

            return View(customers);
        }



//        private IEnumerable<Customer> getCustomers()
//        {
//            return new List<Customer>
//            {
//                new Customer { Id = 1, Name = "John Smith" },
//                new Customer { Id = 2, Name = "Mary Williams" }
//            };
//        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MemberShipTypes = _context.MemberShipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}