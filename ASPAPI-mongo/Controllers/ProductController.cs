using ASPAPI_mongo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPAPI_mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        OrderDbsContext myCollections = new OrderDbsContext();


        public IEnumerable<Product> Get()
        {
            return myCollections.Products;
        }

            [HttpPost]
        public void Post(ProductSubset newProductSubset)
        {
            Product newProduct = new Product();
            newProduct.ProductName = newProductSubset.productName;
            newProduct.ProductDescription = newProductSubset.productDesc;
            newProduct.ProductRating = newProductSubset.productRating;

            myCollections.Add(newProduct);
            myCollections.SaveChanges();
        }

        public class ProductSubset
        {

            public string productName { get; set; }
            public string productDesc { get; set; }
            public int productRating { get; set; }
        }
    }
}
