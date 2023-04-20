using ASPAPI_mongo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPAPI_mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderDbsContext myCollections = new OrderDbsContext();


        [HttpGet]
        public IEnumerable<OrderDetail> Get()
        {
            List<OrderDetail> myList = new List<OrderDetail>();

            var orderQuery = from eachorder in myCollections.Orders
                             select new
                             {

                                 eachorder.Customer.CustomerName,
                                 eachorder.Customer.PhoneNumber,
                                 eachorder.Product.ProductName,
                                 eachorder.Product.ProductDescription,
                                 eachorder.Product.ProductRating,
                                 eachorder.TransactionType

                             };

            foreach (var item in orderQuery)
            {
                OrderDetail newOrderDetail = new OrderDetail();
                //newOrderDetail.orderID = item.OrderId;
                newOrderDetail.customerName = item.CustomerName;
                newOrderDetail.phoneNumber = item.PhoneNumber;
                newOrderDetail.productName = item.ProductName;
                newOrderDetail.productDesc = item.ProductDescription;
                newOrderDetail.productRating = item.ProductRating;
                newOrderDetail.transactionType = item.TransactionType;
                myList.Add(newOrderDetail);
            }
            return myList;

        }




        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]

        public void Post(OrderSubset newOrderSubset)
        {
            Order newOrder = new Order();

            newOrder.CustomerId = newOrderSubset.customerID;
            newOrder.ProductId = newOrderSubset.productID;
            newOrder.TransactionType = newOrderSubset.transactionType;

            myCollections.Add(newOrder);
            myCollections.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


    public class OrderDetail
    {

        public string customerName { get; set; }
        public int phoneNumber { get; set; }
        public string productName { get; set; }
        public string productDesc { get; set; }
        public int productRating { get; set; }
        public string transactionType { get; set; }
    }

    public class OrderSubset
    {

        public int customerID { get; set; }
        public int productID { get; set; }
        public string transactionType { get; set; }
    }
}





