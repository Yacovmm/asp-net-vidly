using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }


        //GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        //GET /api/customers/1
        public CustomerDto GeCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null)
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());

            return Mapper.Map<Customer, CustomerDto> (customer);
        }

        //POST /api/customers
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpException(HttpStatusCode.BadRequest.ToString());

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return customerDto;
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpException(HttpStatusCode.BadRequest.ToString());

            var customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customerInDb == null)
                throw new HttpException(HttpStatusCode.NotFound.ToString());

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
//            customerInDb.Name = customerDto.Name;
//            customerInDb.BirthDate = customerDto.BirthDate;
//            customerInDb.IsSubscribedToNewsLetter = customerDto.IsSubscribedToNewsLetter;
//            customerInDb.MemberShipTypeId = customerDto.MemberShipTypeId;

            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customerInDb == null)
                throw new HttpException(HttpStatusCode.NotFound.ToString());

            _context.Customers.Remove(customerInDb);

            _context.SaveChanges();
        }
    }
}
