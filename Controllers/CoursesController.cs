using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Models.CourseRepository;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseRepository _repo;


        public CoursesController(CourseRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _repo.GetAllCourseAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _repo.GetByIdAsync(id);
            if (course is null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateById(EntityCourse course)
        {
            var result = await _repo.CreateCourse(course);
            if (course is null) return NotFound();
            return Ok(result);
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, EntityCourse course)
        {
            course.Id = id;
            var result = await _repo.UpdateAsync(course);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _repo.DeleteAsync(id);
            if (!result) return NotFound();
            
            return NoContent();
        }





    }
}