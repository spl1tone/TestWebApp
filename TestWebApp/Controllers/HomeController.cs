using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController (ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy ()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error ()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static List<Product> products = new List<Product>();

        public IActionResult Index ()
        {
            return View(products);
        }

        public IActionResult Create ()
        {
            Product newProduct = new Product();
            return View(newProduct);
        }

        [HttpPost]
        public IActionResult Create (Product product)
        {
            product.Id = products.Count + 1;
            products.Add(product);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddProduct ()
        {
            return RedirectToAction("Create");
        }

    }
}
