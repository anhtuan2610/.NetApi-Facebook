using Facebook_be.Models;
using Facebook_be.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facebook_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly FacebookDbContext _context;
        private readonly ICommentRepository _repository;
        public CommentController(FacebookDbContext context, ICommentRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet("comments")]
        public IActionResult GetAllCommentByPostId(int id)
        {
            var comments = _repository.GetCommentsByPostId(id);
            if (comments == null || !comments.Any())
            {
                return NotFound(new { Message = "No comments found for the specified post." });
            }
            return Ok(comments);
        }
    }
}
