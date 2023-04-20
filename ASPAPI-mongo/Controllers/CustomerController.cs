using ASPAPI_mongo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace ASPAPI_mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {


        OrderDbsContext myCollections = new OrderDbsContext();

        //POST api/Customer
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return myCollections.Customers;
        }

        [HttpPost]
        public void Post(CustomerSubset newCustomerSubset)
        {
            Customer newCustomer = new Customer();
            newCustomer.CustomerName = newCustomerSubset.customerName;
            newCustomer.PhoneNumber = newCustomerSubset.phoneNumber;

            myCollections.Add(newCustomer);
            myCollections.SaveChanges();
        }

        public class CustomerSubset
        {
            public string customerName { get; set; }
            public int phoneNumber { get; set; }
        }
    }
}
