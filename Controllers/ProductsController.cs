using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace id_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var products = new[]
            {
                new { Id = 1, Name = "Product A" },
                new { Id = 2, Name = "Product B" }
            };
            return Ok(products);
        }
    }
}
