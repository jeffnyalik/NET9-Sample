using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Webbs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult getProducts()
        {
            return Ok(new { message = "Product list defined" });
        }
    }
}
