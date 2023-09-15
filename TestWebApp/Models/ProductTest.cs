using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using TestWebApp.Controllers;
using Xunit;

namespace TestWebApp.Models;

public class ProductTest
{
    [Fact]
    public void StartTest ()
    {
        var ProductdbContext = new Mock<ProductDbContext>();
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 11f, Count = 5 },
            new Product { Id = 2, Name = "Product2", Price = 15.99f, Count = 3 },
            new Product { Id = 3, Name = "Product3", Price = 2.33f, Count = 4 },
        };
        var controller = new ProductsApiController(ProductdbContext.Object);
        ProductdbContext.Setup(db => db.Products).ReturnsDbSet(products);

        var result = controller.GetAllProducts() as OkObjectResult;
        var productResult = result.Value as List<Product>;

        Assert.NotNull(result);
        Assert.Equal(3, productResult.Count);
    }

}
