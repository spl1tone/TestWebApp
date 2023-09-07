using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{

    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 11f, Count = 5 },
            new Product { Id = 2, Name = "Product2", Price = 15.99f, Count = 3 },
            new Product { Id = 3, Name = "Product3", Price = 2.33f, Count = 4 },
        };

        [HttpGet]
        public IActionResult GetAllProducts ()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct (int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null) {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct ([FromBody] Product product)
        {
            if (product == null) {
                return BadRequest("Invalid product data");
            }
            product.Id = products.Count + 1;
            products.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct (int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null) {
                return NotFound("Product not found");
            }

            products.Remove(product);

            return NoContent();
        }
    }

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

        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 11f, Count = 5 },
            new Product { Id = 2, Name = "Product2", Price = 15.99f, Count = 3 },
            new Product { Id = 3, Name = "Product3", Price = 2.33f, Count = 4 },
        };

        public IActionResult Index ()
        {
            return View(products);
        }

    }
}
