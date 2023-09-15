using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestWebApp.Models;

namespace TestWebApp.Controllers
{

    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductsApiController (ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllProducts ()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct (int id)
        {
            var product = _context.Products.Find(id);
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

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct (int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) {
                return NotFound("Product not found");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

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
