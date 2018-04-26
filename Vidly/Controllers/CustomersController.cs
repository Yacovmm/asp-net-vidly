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

            var viewModel = new NewCustomerViewModel
            {
                MemberShipTypes = memberShipTypes
            };

            return View(viewModel);
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
    }
}