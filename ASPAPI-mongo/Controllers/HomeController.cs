using ASPAPI_mongo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPAPI_mongo.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            //return View();
            return File("~/index.html", "text/html");
        }

    }
}