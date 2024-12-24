using Entities;
using Entities.Mappers;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Webbs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly RepositoryContext _context;
        public StocksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var results = _context.Stocks.ToList()
                .Select(s => s.ToStockDto());
            return Ok(new { message = results });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(u => u.Id == id);
            if(stock == null)
            {
                return NotFound($"Stock with id: {id} not found");
            }

            return Ok(new {result =  stock.ToStockDto()});
        }

        [HttpPost]
        public async Task<IActionResult>CreateStock(Stock stock)
        {
            await _context.AddAsync(stock);
            var result = await _context.SaveChangesAsync();
            return Ok(result);
        }
    }
}
