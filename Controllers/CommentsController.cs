using Entities.Interfaces;
using Entities.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Webbs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        public CommentsController(ICommentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _repository.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(new { message = commentDto });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _repository.GetByIdAsync(id);
            if(comment == null)
            {
                return NotFound();
            };
            return Ok(comment.ToCommentDto());
        }
    }
}
