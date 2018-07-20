using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class CustomerController : ApiController
    {
        private ApplicationDbContext _context;
        
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }


        //return list of customer
        //GET : /api/customer
       public IEnumerable<CustomerDto> GetCustomers()
        {
            //return _context.Customers.ToList();
            return _context.Customers.Include(c => c.MembershipType).ToList().Select(Mapper.Map<Customer, CustomerDto>);
           
        }


        //this is for single customer
        //GET /api/customer/1
        //public Customer GetCustomer(int id)
         public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                //    throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            //return customer;
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }


        //POST /api/customer
        [HttpPost]
        // If we write PostCustomer on the place of CreateCustomer then we do not need to Add [HttpPost]
        //public CustomerDto CreateCustomer(CustomerDto customerDto)
        //IHttpActionResult is similar to ActionResult in MVC
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            }
                var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
                _context.Customers.Add(customer);
                _context.SaveChanges();


                //return customerDto;
                //Uri is Unified Resource Identifier eg - /api/customer/10
                customerDto.Id = customer.Id;
                return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
            
        }

        //PUT /api /customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

                if(customerInDb == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {

                    //in (customerDto is source object and customerInDb is taget object)
                    // in last method i do not add second argument and i create variable because it creates new object and return it 
                    //But in this one we already have existing object 
                    //we have two ways to write this code
                    //first
                    //Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);

                    //second
                    Mapper.Map(customerDto, customerInDb);

                    //old one
                    //customerInDb.Name = customerDto.Name;
                    //customerInDb.Birthdate = customerDto.Birthdate;
                    //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
                    //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

                    _context.SaveChanges();
                }
            }
        }

        [HttpDelete]
        //DELETE /api/customer/1
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                _context.Customers.Remove(customerInDb);
                _context.SaveChanges();
            }
        }

    }
}
