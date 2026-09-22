using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

using AuuthRepository;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthRepository _repo;


        public AuthorController(AuthRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _repo.GetAllAuthsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var author = await _repo.GetByIdAuthAsync(id);
            if (author is null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateById(AuthorEntity author)
        {
            var result = await _repo.CreateAuth(author);
            if (author is null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AuthorEntity author)
        {
            author.Id = id;
            var result = await _repo.UpdateAsync(author);
            if (result is null) return NotFound();
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _repo.DeleteAuthAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }

    }
}