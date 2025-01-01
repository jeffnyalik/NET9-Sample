using Entities;
using Entities.Dto.Stock;
using Entities.Interfaces;
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
        private readonly IStockRepository _stockRepository;
        public StocksController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stock = await _stockRepository.GetAllAsync();
            var result = stock.Select(s => s.ToStockDto());
            return Ok(new { message = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if(stock == null)
            {
                return NotFound($"Stock with id: {id} not found");
            }

            return Ok(new {result =  stock.ToStockDto()});
        }

        [HttpPost]
        public async Task<IActionResult>CreateStock(Stock stock)
        {
            var result = await _stockRepository.CreateAsync(stock);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto updateStockDto)
        {
            var result = await _stockRepository.UpdateAsync(id, updateStockDto);
            if(result == null)
            {
                return NotFound();
            }
            
            return Ok(new {message =  result.ToStockDto()});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteStock(int id)
        {
            var result = await _stockRepository.DeleteAsync(id);
            if(result == null)
            {
                return NotFound();
            }

            return Ok(new { message = "Stock deleted successfully" });
        }
    }
}
