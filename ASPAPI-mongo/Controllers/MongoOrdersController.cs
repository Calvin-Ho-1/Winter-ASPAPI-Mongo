using ASPAPI_mongo.Models;
using ASPAPI_mongo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ASPAPI_mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoOrdersController : ControllerBase
    {
        private readonly MongoOrdersService _ordersService;
        public MongoOrdersController(MongoOrdersService ordersService) =>
            _ordersService = ordersService;

        [HttpGet]
        public async Task<List<MongoOrder>> Get() =>
            await _ordersService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<MongoOrder>> Get(string id)
        {
            var order = await _ordersService.GetAsync(id);
            if (order is null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProtoOrder newOrder)
        {
            MongoOrder tempMongoOrder = new MongoOrder();
            tempMongoOrder.customerName = newOrder.customerName;
            tempMongoOrder.phoneNumber = newOrder.phoneNumber;
            tempMongoOrder.productName = newOrder.productName;
            tempMongoOrder.productDesc = newOrder.productDesc;
            tempMongoOrder.productRating = newOrder.productRating;
            tempMongoOrder.transactionType = newOrder.transactionType;
            await _ordersService.CreateAsync(tempMongoOrder);
            return CreatedAtAction(nameof(Get), newOrder);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, MongoOrder updatedOrder)
        {
            var order = await _ordersService.GetAsync(id);
            if (order is null)
            {
                return NotFound();
            }
            updatedOrder.Id = order.Id;
            await _ordersService.UpdateAsync(id, updatedOrder);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _ordersService.GetAsync(id);
            if (order is null)
            {
                return NotFound();
            }
            await _ordersService.RemoveAsync(id);
            return NoContent();
        }

        public class ProtoOrder
        {
            public string customerName { get; set; }
            public int phoneNumber { get; set; }
            public string productName { get; set; }
            public string productDesc { get; set; }
            public int productRating { get; set; }
            public string transactionType { get; set; }
        }
    }
}
